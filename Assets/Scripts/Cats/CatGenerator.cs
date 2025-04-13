using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class CatGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pod;
    [SerializeField] private GameObject CatsParent;
    [SerializeField] private GameObject cat;
    [SerializeField] private GameObject CatInfoMenu;

    [SerializeField] private CatData[] catData;
    private Vector3 spawnPoint;
    private List<int> previouslyUsedCatIndex;
    private List<GameObject> CatsCurrentlyInShelter;

    private int catDataLength;

    private void Start()
    {
        
        catDataLength = catData.Length;
        spawnPoint = new Vector3 (0, 0, 0);
    }


     void CreateCat()
    {
        Debug.Log("Create Cat");
        //get a random number
        int getRandomIndex = Random.Range(0, catDataLength);

        //Create a new pod
        GameObject newPod = Instantiate(pod, spawnPoint, Quaternion.identity);
        newPod.transform.SetParent(CatsParent.transform);

        //Create a new cat and put that inside the pod
        GameObject newCat = Instantiate(catData[getRandomIndex].catPrefab, spawnPoint, Quaternion.identity);
         newCat.transform.SetParent(newPod.transform);

        
        //CatPodInteraction podInteraction = newPod.GetComponent<CatPodInteraction>();
      //  podInteraction.CatOptionsMenu = CatOptionsMenu;

        
        if (newCat.TryGetComponent<DisplayCatInformation>(out var catInfo))
        {
            catInfo.catData = catData[getRandomIndex];
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateCat();
        }
    }
}
