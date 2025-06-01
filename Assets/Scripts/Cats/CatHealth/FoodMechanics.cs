using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FoodMechanics : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] GameObject foodBagZipped;
    [SerializeField] GameObject food;
    [SerializeField] GameObject EmptyBowl;
    [SerializeField] GameObject foodBar;
    [SerializeField] GameObject CatFoodParent;
    [SerializeField] GameObject WARNINGUI;
    [Header("Stock")]
    public TextMeshProUGUI FoodStock;
    [Header("Sprites")]
    [SerializeField] Sprite FilledBowl;
    [SerializeField] Sprite foodBagUnzipped;
    [SerializeField] Sprite foodBagzipped;


    [Header("Data")]
    public Vector2 biscuitPosition = new Vector2(-125f, 45f);
    public float jitterAmount = 7f;
    public int amountOfBiscuits = 5;
    private bool isBeingFed = false;
    private int clicks = 0;

    private enum FoodBagState { Idle, Dispensing }
    private FoodBagState currentState = FoodBagState.Idle;

    //Need to have a function to click on the cat food bag
    public void PickUpCatFood()
    {

        if (PlayerController.Instance.CatFood > 0 && currentState == FoodBagState.Idle)
        {
            //the bag needs to go upwards as if its tipping over
            isBeingFed = true;
            currentState = FoodBagState.Dispensing;
            ChangePosition(new Vector2(130, 161), new Vector3(0f, 0f, 45f));

            //Change bag
            if (foodBagZipped != null && foodBagZipped.GetComponent<Image>() != null)
            {
                foodBagZipped.GetComponent<Image>().sprite = foodBagUnzipped;
            }
            else
            {
                Debug.Log("Image component not found");
            }
            PlayerController.Instance.CatFood -= 1;
            //   foodBagZipped.GetComponent<Button>().onClick.RemoveAllListeners();
            // foodBagZipped.GetComponent<Button>().onClick.AddListener(ShowBiscuits);

            StartCoroutine(SwitchToShowBiscuits()); //this stops error from deleting stock
        }
        else
        {
            Debug.Log("No cat food");
            if(!isBeingFed)
            {
                WARNINGUI.SetActive(true);

            }


        }
    }

    private IEnumerator SwitchToShowBiscuits()
    {
        yield return null;
        foodBagZipped.GetComponent<Button>().onClick.RemoveAllListeners();
        foodBagZipped.GetComponent<Button>().onClick.AddListener(ShowBiscuits);
    }

    //changes position and rotation
    private void ChangePosition(Vector2 position, Vector3 rotation)
    {
        foodBagZipped.GetComponent<RectTransform>().localPosition = position;
        foodBagZipped.GetComponent<RectTransform>().rotation = Quaternion.Euler(rotation);

    }


    //food needs to fall out
    private void ShowBiscuits()
    {
        CatOptionsMenuController catOptions = gameObject.GetComponent<CatOptionsMenuController>();
        DisplayCatInformation cat = PlayerController.Instance.catViewing.GetComponent<DisplayCatInformation>();
        foodBagZipped.GetComponent<AudioSource>().Play();
        for (int i = 0; i < amountOfBiscuits; i++)
        {
            GameObject biscuit = Instantiate(food);
            biscuit.transform.SetParent(foodBagZipped.transform);
            biscuit.transform.localPosition = GeneratePositions();
           

           
        }
        clicks++;
        if (clicks % 3 == 0)
        {
            if (cat.catData.hunger < 1) //if less than 1 then update
            {
                cat.catData.hunger += 0.1f;
                catOptions.ToggleMenu(catOptions.foodMenu);
            }
        }

        if (cat.catData.hunger >= 1)
        {
            ShowFullBowl();
        }
    }

    public Vector2 GeneratePositions()
    {
        float offsetX = Random.Range(-jitterAmount, jitterAmount);
        float offsetY = Random.Range(-jitterAmount, jitterAmount);
        return biscuitPosition + new Vector2(offsetX, offsetY);
    }

    public void ShowFullBowl()
    {

        EmptyBowl.GetComponent<Image>().sprite = FilledBowl;
        Cursor.visible = true;
        isBeingFed = false;
        foodBagZipped.GetComponent<Image>().sprite = foodBagzipped;
        ChangePosition(new Vector2(365, -204), new Vector3(0f, 0f, 0f));
        clicks = 0;
        currentState = FoodBagState.Idle;
        //change onclick again
        foodBagZipped.GetComponent<Button>().onClick.RemoveAllListeners();
        foodBagZipped.GetComponent<Button>().onClick.AddListener(PickUpCatFood);

        //rewards
        AdoptionShelterReputation.Instance.SetCurrentPoints(30); //get 30 points!

    }
    private void Update()
    {
        FoodStock.text = "Stock: " + PlayerController.Instance.CatFood.ToString();


        if (isBeingFed)
         {
   
          
            Cursor.visible = false;
          
            foodBagZipped.transform.position = Input.mousePosition;

        }


      
    }

}
