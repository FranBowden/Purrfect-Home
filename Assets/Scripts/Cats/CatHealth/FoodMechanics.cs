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
    [Header("Sprites")]
    [SerializeField] Sprite FilledBowl;
    [SerializeField] Sprite foodBagUnzipped;
    [SerializeField] Sprite foodBagzipped;


    [Header("Data")]
    public Vector2 biscuitPosition = new Vector2(-125f, 45f);
    public float jitterAmount = 7f;
    public int amountOfBiscuits = 5;
    public float spawnInterval = 0.03f;
    public int maxClicks = 20;
    private bool isBeingFed = false;
    private int clicks = 0;


    //Need to have a function to click on the cat food bag
    public void PickUpCatFood()
    {
        //the bag needs to go upwards as if its tipping over
        isBeingFed = true;
        
        ChangePosition(new Vector2(130, 161), new Vector3(0f, 0f, 45f));

        //Change bag
        if (foodBagZipped != null && foodBagZipped.GetComponent<Image>() != null)
        {
            foodBagZipped.GetComponent<Image>().sprite = foodBagUnzipped;
        } else
        {
            Debug.Log("Image component not found");
        }
      
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
        for (int i = 0; i < amountOfBiscuits; i++)
        {
            GameObject biscuit = Instantiate(food);
            biscuit.transform.SetParent(foodBagZipped.transform);
            biscuit.transform.localPosition = GeneratePositions();
           

           
        }
        clicks++;
        if(clicks == maxClicks)
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

    }
    private void Update()
    {
     

        if (isBeingFed)
         {
   
            foodBagZipped.GetComponent<Button>().onClick.RemoveAllListeners();
            foodBagZipped.GetComponent<Button>().onClick.AddListener(ShowBiscuits);
            Cursor.visible = false;
          
            foodBagZipped.transform.position = Input.mousePosition;

        }


      
    }

}
