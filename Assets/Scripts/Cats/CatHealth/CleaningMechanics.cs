using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CleaningMechanics : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject Bag;
    [SerializeField] GameObject litter;
    [SerializeField] GameObject poop;
    [SerializeField] GameObject tray;
    [SerializeField] GameObject cleaningBar;
    [SerializeField] GameObject cleaningParentMenu;
    public TextMeshProUGUI CleanStock;
    [Header("Sprites")]

    [SerializeField] Sprite litterTray;
    [SerializeField] Sprite EmptylitterTray;
    [SerializeField] Sprite ZippedBag;
    [SerializeField] Sprite UnZippedBag;
    [Header("Audio")]
    [SerializeField] private AudioSource Pop;
    public Vector2 poopPosition = new Vector2(-0, -9);
    public Vector2 litterPosition = new Vector2(-125f, 45f);
    public float jitterAmountT = 35f;
    public float jiggerAmountB = 7f;
    public int clicks = 0;
    public int amountOfLitter = 5;
    List<GameObject> wasteAmount = new List<GameObject>();
    public int numWaste = 3;

    private CatOptionsMenuController catOptions;


    private bool isLitterEmpty = false;
    private bool GettingNewLitter = false;

    

    private void Start()
    {
        catOptions = GetComponent<CatOptionsMenuController>();
    }
    private void Update()
    {

        CleanStock.text = "Stock: " + PlayerController.Instance.CatLitter.ToString();

        if (GettingNewLitter)
        {

            Bag.GetComponent<Button>().onClick.RemoveAllListeners();
            Bag.GetComponent<Button>().onClick.AddListener(ShowLitter);
            Cursor.visible = false;

            Bag.transform.position = Input.mousePosition;
        }

    }

    public void PickUpLitterBag()
    {
        if (PlayerController.Instance.CatLitter > 0 && isLitterEmpty)
        {
            //the bag needs to go upwards as if its tipping over
            GettingNewLitter = true;
            isLitterEmpty = false;
            ChangePosition(new Vector2(130, 161), new Vector3(0f, 0f, 45f));

            // Change bag
            if (Bag != null && Bag.GetComponent<Image>() != null)
            {
                Bag.GetComponent<Image>().sprite = UnZippedBag;
            }
            else
            {
                Debug.Log("Image component not found");
            }
            PlayerController.Instance.CatLitter -= 1;
        }
        else
        {
            Debug.Log("No cat litter");

        }
    }

    private void ChangePosition(Vector2 position, Vector3 rotation)
    {
        Bag.GetComponent<RectTransform>().localPosition = position;
        Bag.GetComponent<RectTransform>().rotation = Quaternion.Euler(rotation);

    }
    private void ShowLitter()
    {
        CatOptionsMenuController catOptions = gameObject.GetComponent<CatOptionsMenuController>();
        DisplayCatInformation cat = PlayerController.Instance.catViewing.GetComponent<DisplayCatInformation>();

        for (int i = 0; i < amountOfLitter; i++)
        {
            GameObject Litter = Instantiate(litter);
            Litter.transform.SetParent(Bag.transform);
            Litter.transform.localPosition = GeneratePositions();


        }
        clicks++;
        if (clicks % 3 == 0)
        {
            Reward(0);
        }

        if (Mathf.Approximately(cat.catData.hygiene, 1f) || cat.catData.hygiene >= 1)
        {
          ShowFullLitterTray();
        }
    }

    public void ShowFullLitterTray()
    {
        
        Cursor.visible = true;
        isLitterEmpty = false;
        GettingNewLitter = false;
        if (Bag != null && Bag.GetComponent<Image>() != null)
        {
            Bag.GetComponent<Image>().sprite = ZippedBag;
        }
        if (tray != null && tray.GetComponent<Image>() != null)
        {
            tray.GetComponent<Image>().sprite = litterTray;
        }
        ChangePosition(new Vector2(45, -211), new Vector3(0f, 0f, 0f));
        clicks = 0;

        //change onclick again
        Bag.GetComponent<Button>().onClick.RemoveAllListeners();
        Bag.GetComponent<Button>().onClick.AddListener(PickUpLitterBag);

        //rewards
        AdoptionShelterReputation.Instance.SetCurrentPoints(30); //get 30 points!

    }

    public Vector2 GeneratePositions()
    {
        float offsetX = Random.Range(-jiggerAmountB, jiggerAmountB);
        float offsetY = Random.Range(-jiggerAmountB, jiggerAmountB);
        return litterPosition + new Vector2(offsetX, offsetY);
    }

    public void GeneratePoop()
    {
        //resetting 
        foreach (GameObject poop in wasteAmount)
        {
            Destroy(poop);
        }
        wasteAmount.Clear();

        tray.GetComponent<Image>().sprite = litterTray;


        for (int i = 0; i < 3; i++)
        {

            GameObject Poop = Instantiate(poop);
            Poop.transform.SetParent(tray.transform);
            Poop.transform.localPosition = GenerateTrayPositions();
            wasteAmount.Add(Poop);
            Poop.GetComponent<Button>().onClick.AddListener(() => RemovePoop(Poop));
        }
    }


    public Vector2 GenerateTrayPositions()
    {
        float offsetX = Random.Range(-jitterAmountT, jitterAmountT);
        return poopPosition + new Vector2(offsetX, 0);
    }

    public void RemovePoop(GameObject poop)
    {
        Destroy(poop);
        wasteAmount.Remove(poop);
        Pop.Play();

        Reward(5);

        if (wasteAmount.Count == 0)
        {

            tray.GetComponent<Image>().sprite = EmptylitterTray;
            isLitterEmpty = true;
        }

    }

    void Reward(int reward)
    {
        DisplayCatInformation catInfo = PlayerController.Instance.catViewing.GetComponent<DisplayCatInformation>();
        CatOptionsMenuController catOptions = gameObject.GetComponent<CatOptionsMenuController>();

        if(reward > 0) //if reward is more than 0 
         {
            AdoptionShelterReputation.Instance.SetCurrentPoints(reward);

        }
        if (catInfo != null && catInfo.catData.hygiene < 1)
        {
            catInfo.catData.hygiene += 0.1f; //increase by one
            catOptions.ToggleMenu(catOptions.cleanMenu);
        }
    }
}
