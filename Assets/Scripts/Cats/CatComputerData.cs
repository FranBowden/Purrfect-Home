using System;
using System.Collections.Generic;
using TMPro;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.UI;

public class CatComputerData : MonoBehaviour
{
    //
    [SerializeField] private CatList catlist;
    [SerializeField] GameObject CatPrefab;
    [SerializeField] AudioSource click;
    [SerializeField] GameObject prefabCatListingItem;
    [SerializeField] Transform prefabCatListing;
    [SerializeField] GameObject prefabCatParent;
    [SerializeField] GameObject WarningMessage;

    public  Transform catPodsPositions;
  
    private GameObject[] CatListing; //stores the cats listed on computer for that day
    private bool[] podStatus;
    private bool[] listingCatStatus;
    public Transform[] spawnPositions;

    private readonly int numberOfListings = 3;
    public int numberOfPods = 3;
    private List<int> chosenCatIndices;
    private List<int> previousCats = new List<int>();

    

    public static CatComputerData Instance { get; private set; }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        listingCatStatus = new bool[numberOfListings];
        CatListing = new GameObject[numberOfListings];
        chosenCatIndices = new List<int>();
       
 

    }
    private void Start()
    {
        numberOfPods = Mathf.Clamp(PlayerController.Instance.CatPod, 3, 6);
        spawnPositions = GetSpawnPoints(catPodsPositions, numberOfPods);

        podStatus = new bool[numberOfPods];


        for (int i = 0; i < numberOfPods; i++)
        {
            podStatus[i] = false; //false means the pod is free
        }


        for (int i = 0; i < listingCatStatus.Length; i++)
        {
            listingCatStatus[i] = false; //false means the list is free
        }

        RefillCatSuggestions();
    }

    private void Update()
    {
        if(AdoptionStats.Instance.CatsShelteredToday == 3)
        {
            WarningMessage.SetActive(true);
        } else
        {
            WarningMessage.SetActive(false);

        }
    }

    public Transform[] GetSpawnPoints(Transform parent, int maxPods)
    {
        int total = Mathf.Max(3, maxPods); 
        Debug.Log("Total pods:" + total);
        Transform[] children = new Transform[total];

        for (int i = 0; i < total; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }

    private int GetUniqueCatIndex()
    {
        
        if (previousCats.Count >= catlist.catData.Length)
        {
            Debug.LogWarning("All unique cats have been used");
            previousCats.Clear();
        }

        int index;
        bool isDuplicate;

        do
        {
            index = UnityEngine.Random.Range(0, catlist.catData.Length);
            isDuplicate = previousCats.Contains(index); 
        } while (isDuplicate);
        previousCats.Add(index);
        return index;
    }

    public void RefillCatSuggestions()
    {
        for (int i = 0; i < listingCatStatus.Length; i++)
        {
            try
            {

                if (!listingCatStatus[i]) //if there is a space free
                {
                 
                    //create a new listing
                    GameObject newCatListing = Instantiate(prefabCatListingItem);
                    newCatListing.transform.SetParent(prefabCatListing.transform, false);

                    CatListing[i] = newCatListing;

                    int catIndex = GetUniqueCatIndex(); //gets unique cat in the list

                    chosenCatIndices.Add(catIndex); //an array for the chosen cats
                  
                    listingCatStatus[i] = true;

                    SetCatDataToList(i);

                    var buttonTransform = CatListing[i].transform.Find("Button");
                    if (buttonTransform == null)
                    {
                        continue;
                    }

                    if (buttonTransform.TryGetComponent<Button>(out var btn))
                    {
                        int index = i;
                        btn.onClick.AddListener(() => CatAccepted(index));
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception in RefillCatSuggestions loop at index {i}: {e}");
            }
        }
    }

    public void ClearCatListings()
    {
      
        if (chosenCatIndices == null)
        {
            chosenCatIndices = new List<int>();
        }
        else
        {
            chosenCatIndices.Clear();
        }

        if (CatListing == null) return;

        for (int i = 0; i < CatListing.Length; i++)
        {
            if (CatListing[i] != null)
            {
                var buttonTransform = CatListing[i].transform.Find("Button");
                if (buttonTransform != null && buttonTransform.TryGetComponent<Button>(out var btn))
                {
                    btn.onClick.RemoveAllListeners();
                }
                Destroy(CatListing[i]);
                listingCatStatus[i] = false;
                CatListing[i] = null;
            }
        }

        CatListing = new GameObject[numberOfListings];
    }
    private void SetCatDataToList(int index) //assigns all the data from catdata into catlist ui data
    {

        Image catImage = CatListing[index].transform.Find("Frame/CatImage").GetComponent<Image>();
        TextMeshProUGUI catName = CatListing[index].transform.Find("Cat Information/CatName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catDescription = CatListing[index].transform.Find("Cat Information/CatDescription").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catAge = CatListing[index].transform.Find("Cat Information/Cat Age").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catValue = CatListing[index].transform.Find("Cat Information/Cat Value").GetComponent<TextMeshProUGUI>();


        catImage.sprite = catlist.catData[chosenCatIndices[index]].catSprite;

        catName.text = catlist.catData[chosenCatIndices[index]].catName.ToString();
        catDescription.text = catlist.catData[chosenCatIndices[index]].catListingDescription.ToString();
        catAge.text = "Age: " + catlist.catData[chosenCatIndices[index]].catAge.ToString();
        catValue.text = "Value: " + catlist.catData[chosenCatIndices[index]].value.ToString();
    }


    private void CatAccepted(int listingIndex)
    {

        click.Play();
        if (CatListing[listingIndex] != null)
        {
            Transform nameTransform = CatListing[listingIndex].transform.Find("Cat Information/CatName");
            if (nameTransform != null)
            {
                TextMeshProUGUI nameText = nameTransform.GetComponent<TextMeshProUGUI>();
              
            }


            int FreePod = GetFreePodIndex();

            if (FreePod >= 0 && FreePod < numberOfPods) //there is a free pod for the cat to enter!
            {
                CatSpawnedInPod(chosenCatIndices[listingIndex], FreePod);
                AdoptionStats.Instance.numCatsPlacedInShelter++;
                Destroy(CatListing[listingIndex]);


                if (listingIndex >= 0 && listingIndex < listingCatStatus.Length)
                {
                    listingCatStatus[listingIndex] = false;
                }
              
              MarkPodAsOccupied(FreePod);


                AdoptionShelterReputation.Instance.SetCurrentPoints(10); //10 points for just accepting the cat into the shelter
                AdoptionStats.Instance.CatsShelteredToday++;

            }
            else if (FreePod == -1)
            {
                Debug.Log("No pods are free");
                if (CatListing[listingIndex] != null)
                {
                    Transform warningTransform = CatListing[listingIndex].transform.Find("WarningError");
                    if (warningTransform != null)
                    {
                        GameObject WarningUI = warningTransform.gameObject;
                        WarningUI.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("WarningError child not found!");
                    }

                }
                Debug.Log("Number of pods:" + numberOfPods);

                for (int i = 0; i < numberOfPods; i++)
                {
                    Debug.Log("Pod Status: " + i + " = " + podStatus[i]);
                }
            }
        }
    }


    private void CatSpawnedInPod(int index, int podIndex)
    {

        GameObject newCat = Instantiate(CatPrefab, spawnPositions[podIndex].position, Quaternion.identity);
        newCat.transform.SetParent(prefabCatParent.transform);

        newCat.GetComponent<SpriteRenderer>().sprite = catlist.catData[index].catSprite;

        if (newCat.TryGetComponent<DisplayCatInformation>(out var catInfo))
        {
            catInfo.catData = catlist.catData[index];

            catInfo.catData.catPodAssigned = podIndex; //assign the cat a pod

        }


    }
    public void UpdatePodStatusArray(int newPodCount)
    {
        bool[] newStatus = new bool[newPodCount];

        for (int i = 0; i < newStatus.Length; i++)
        {
            if (i < podStatus.Length)
                newStatus[i] = podStatus[i];
            else
                newStatus[i] = false; 
        }

        podStatus = newStatus;
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
