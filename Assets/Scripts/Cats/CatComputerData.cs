using System;
using System.Collections.Generic;
using TMPro;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.UI;

public class CatComputerData : MonoBehaviour
{ 
    public CatData[] catData;

    [SerializeField] GameObject CatPrefab;

    [SerializeField] GameObject prefabCatListingItem;
    [SerializeField] Transform prefabCatListing;
    [SerializeField] GameObject prefabCatParent;
    [SerializeField] Transform catPodsPositions;
  
    private GameObject[] CatListing; //stores the cats listed on computer for that day
    private bool[] podStatus;
    private bool[] listingCatStatus;
    private Transform[] spawnPositions;

    private readonly int numberOfListings = 3;
    private readonly int numberOfPods = 3;
    
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
        podStatus = new bool[numberOfPods];
        spawnPositions = GetSpawnPoints(catPodsPositions);
  

        for (int i = 0; i < podStatus.Length; i++)
        {
            podStatus[i] = false; //false means the pod is free
        }

        for (int i = 0; i < listingCatStatus.Length; i++)
        {
            listingCatStatus[i] = false; //false means the list is free
        }
    }
    private void Start()
    {
        

        RefillCatSuggestions();
    }

    private Transform[] GetSpawnPoints(Transform parent)
    {
        Transform[] children = new Transform[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }

    private int GetUniqueCatIndex()
    {
        if (previousCats.Count >= catData.Length)
        {
            Debug.LogWarning("All unique cats have been used. Consider resetting previousCats.");
            previousCats.Clear(); // or handle this however your game logic requires
        }

        int index;
        bool isDuplicate;

        do
        {
            index = UnityEngine.Random.Range(0, catData.Length);
            isDuplicate = previousCats.Contains(index); 
        } while (isDuplicate);

        return index;
    }
    public void RefillCatSuggestions()
    {

/*
        try
        {
            ClearCatListings(); //clear previous listings
        }
        catch (Exception e)
        {
            Debug.LogError("Exception in ClearCatListings: " + e);
            return; //stop early if this errors
        }
*/


        for (int i = 0; i < listingCatStatus.Length; i++)
        {
            try
            {

                if (!listingCatStatus[i])
                {
                 
                    GameObject newCatListing = Instantiate(prefabCatListingItem);
                    newCatListing.transform.SetParent(prefabCatListing.transform, false);

                    CatListing[i] = newCatListing;

                    int catIndex = GetUniqueCatIndex();
                //int catIndex = UnityEngine.Random.Range(0, catData.Length);


                    chosenCatIndices.Add(catIndex);
                    previousCats.Add(catIndex);


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

    private void ClearCatListings()
    {
      
        if (chosenCatIndices == null)
        {
         //   Debug.LogWarning("chosenCatIndices was null");
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


        catImage.sprite = catData[chosenCatIndices[index]].catSprite;

        catName.text = catData[chosenCatIndices[index]].catName.ToString();
        catDescription.text = catData[chosenCatIndices[index]].catListingDescription.ToString();
        catAge.text = "Age: " + catData[chosenCatIndices[index]].catAge.ToString();
    }


    private void CatAccepted(int listingIndex)
    {
 
        
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

                for (int i = 0; i < podStatus.Length; i++)
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

        newCat.GetComponent<SpriteRenderer>().sprite = catData[index].catSprite;

        if (newCat.TryGetComponent<DisplayCatInformation>(out var catInfo))
        {
            catInfo.catData = catData[index];

            catInfo.catData.catPodAssigned = podIndex; //assign the cat a pod

        }


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
