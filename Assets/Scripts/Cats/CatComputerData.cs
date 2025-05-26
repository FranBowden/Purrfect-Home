using System;
using System.Collections.Generic;
using TMPro;
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
    private int numberOfPods = 3;
    private int CatDataLength = 0;
    private List<int> chosenCatIndices;

    private void Awake()
    {
        listingCatStatus = new bool[numberOfListings];
        CatListing = new GameObject[numberOfListings];
        chosenCatIndices = new List<int>();
        podStatus = new bool[numberOfPods];
        spawnPositions = GetSpawnPoints(catPodsPositions);
        CatDataLength = catData.Length;

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

    public void RefillCatSuggestions()
    {
        ClearCatListings(); //destroy/clear previous cat listings

        Debug.Log("listcating cat status length = " + listingCatStatus.Length);
        //Refill cat listing
        for (int i = 0; i < listingCatStatus.Length; i++)
        {   
            if (!listingCatStatus[i]) //if there is a listing spot avaiable then take it
            {
                //create a new listening prefab 
                GameObject newCatListing = Instantiate(prefabCatListingItem);
                newCatListing.transform.SetParent(prefabCatListing, false);

                CatListing[i] = newCatListing; //assigning cat list prefab gameobject to catlisting UI array

                int catIndex = UnityEngine.Random.Range(0, CatDataLength);  //gets a random number

                chosenCatIndices.Add(catIndex); //stores chosen cats in an array to reference later if they get accepted

                SetCatDataToList(i);
                listingCatStatus[i] = true; //set listing to true


                // if (CatListing[i] == null) continue;

                var buttonTransform = CatListing[i].transform.Find("Button");
                if (buttonTransform.TryGetComponent<Button>(out var btn))
                {
                    int index = i;
                    btn.onClick.AddListener(() => CatAccepted(index));
                }


            }
        }
        
    }

    private void ClearCatListings()
    {
      
        if (chosenCatIndices == null)
        {
            Debug.LogWarning("chosenCatIndices was null");
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
        AdoptionShelterReputation.Instance.SetCurrentPoints(10); //10 points for just accepting the cat into the shelter
           
        if (CatListing[listingIndex] != null)
        {
            var nameTransform = CatListing[listingIndex].transform.Find("Cat Information/CatName");
            if (nameTransform != null)
            {
                var nameText = nameTransform.GetComponent<TextMeshProUGUI>();
                if (nameText != null)
                {
                    Debug.Log("You accepted " + nameText.text);

                }

            }


            int FreePod = GetFreePodIndex();

            if (FreePod >= 0)
            {
                CatSpawnedInPod(chosenCatIndices[listingIndex], FreePod);
                AdoptionStats.Instance.numCatsPlacedInShelter++;
                Destroy(CatListing[listingIndex]);
                listingCatStatus[listingIndex] = false;
                MarkPodAsOccupied(FreePod);

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
            Debug.Log("Marking pod " + index + " as occupied");
            podStatus[index] = true; 
        }
        else
        {
            Debug.Log("Tried to mark invalid pod index: " + index);
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
