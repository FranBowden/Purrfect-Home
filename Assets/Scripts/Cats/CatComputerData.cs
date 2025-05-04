using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatComputerData : MonoBehaviour
{

    public CatData[] catData;
    [SerializeField] GameObject prefabCatListingItem;
    [SerializeField] Transform prefabCatListing;
    [SerializeField] GameObject prefabCatParent;
    private GameObject[] CatListing;
    private Vector2[] spawnPositions = new Vector2[]
    {
        new Vector2(-6f, 3.8f),
        new Vector2(-9f, 3.8f),
        new Vector2(-12f, 3.8f),
      //  new Vector2(-15f, 3.8f),
    };
    private bool[] podStatus = new bool[3];
    private bool[] listingCatStatus = new bool[3];


    private void Start()
    {
        CatListing = new GameObject[catData.Length];

        for (int i = 0; i < podStatus.Length; i++)
        {
            podStatus[i] = false; //false means the pod is free
        }
        for (int i = 0; i < listingCatStatus.Length; i++)
        {
            listingCatStatus[i] = false; //false means the list is free
        }

        RefillCatSuggestions();
        for (int i = 0; i < CatListing.Length; i++)
        {

            if (CatListing[i].transform.Find("Button").TryGetComponent<Button>(out var btn))
            {
                int index = i;
                btn.onClick.AddListener(() => CatAccepted(index)); //listens to which button has been pressed
            }
        }
    }

    private void Update()
    {
      
    }
    private void RefillCatSuggestions()
    {
              //int i and listingCatStatus.length need to be changed e.g. 0 , 3, 3 to 6, 6 to 9 - there always needs to be a 3 or something like that?
              //the index needs to be updated every time? otherwise its going 0 1 2 0 1 2 resulting in the same cats.
        for (int i = 0; i < listingCatStatus.Length; i++) 
        {
            if (!listingCatStatus[i]) //if there is a listing spot avaiable then take it
            {
                GameObject newCatListing = Instantiate(prefabCatListingItem);
                newCatListing.transform.SetParent(prefabCatListing, false);

                CatListing[i] = newCatListing; //assigning list to catlisting UI array

                SetCatDataToList(i);
                listingCatStatus[i] = true; //set listing to true
                
            }
           
        }
    }

    private void SetCatDataToList(int index) //assigns all the data from catdata into catlist ui data
    {
        
        Image catImage = CatListing[index].transform.Find("Frame/CatImage").GetComponent<Image>();
        TextMeshProUGUI catName = CatListing[index].transform.Find("Cat Information/CatName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catDescription = CatListing[index].transform.Find("Cat Information/CatDescription").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catAge = CatListing[index].transform.Find("Cat Information/Cat Age").GetComponent<TextMeshProUGUI>();


        catImage.sprite = catData[index].catPrefab.GetComponent<SpriteRenderer>().sprite;
        catName.text = catData[index].catName.ToString();
        catDescription.text = catData[index].catDescription.ToString();
        catAge.text = "Age: " + catData[index].catAge.ToString();
    }


    private void CatAccepted(int index)
    {
        Debug.Log("You accepted " + CatListing[index].transform.Find("Cat Information/CatName").GetComponent<TextMeshProUGUI>().text); //shows the name of the selected cat


        //Set cat to pod
        int FreePod = GetFreePodIndex();

        if (FreePod != -1)
        {
            CatSpawnedInPod(index, FreePod);
            Destroy(CatListing[index]);
            listingCatStatus[index] = false;


        }
        else if (FreePod == -1)
        {
            Debug.Log("No pods are free.");
        }
     
    }


    private void CatSpawnedInPod(int index, int podIndex)
    {
        GameObject newCat = Instantiate(catData[index].catPrefab, spawnPositions[podIndex], Quaternion.identity);
        newCat.transform.SetParent(prefabCatParent.transform);
        if (newCat.TryGetComponent<DisplayCatInformation>(out var catInfo))
        {
            catInfo.catData = catData[index];
        }

        MarkPodAsOccupied(podIndex);
    }

    public int GetFreePodIndex()
    {
        for (int i = 0; i < podStatus.Length; i++)
        {
            if (!podStatus[i]) //if a pod is free
            {
                return i; //return pod index
            }
        }

        return -1; //no pod is free
    }

    public void MarkPodAsOccupied(int index)
    {
        if (index >= 0 && index < podStatus.Length)
        {
            podStatus[index] = true; 
        }
    }

    public void MarkPodAsFree(int index)
    {
        if (index >= 0 && index < podStatus.Length)
        {
            podStatus[index] = false; 
        }
    }
}
