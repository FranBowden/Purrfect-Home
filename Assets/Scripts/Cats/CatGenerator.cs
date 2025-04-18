using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class CatGenerator : MonoBehaviour
{
   
    [SerializeField] private GameObject CatsParent;
    [SerializeField] private GameObject pod;
    [SerializeField] private GameObject CatInfoMenu;

    [SerializeField] private CatData[] catData;
  

    private Vector2[] spawnPositions = new Vector2[]
    {
        new Vector2(-6f, 3.8f),
        new Vector2(-9f, 3.8f),
        new Vector2(-12f, 3.8f),
        new Vector2(-15f, 3.8f),
    };

    private GameObject[] spawnedCats;


    private int catDataLength;

    private void Start()
    {
        
        catDataLength = catData.Length;
     
        spawnedCats = new GameObject[spawnPositions.Length];

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            CreateCat(i);
        }
    }


     void CreateCat(int PosIndex)
    {
       // Debug.Log("Create Cat");
        //get a random number
        int getRandomIndex = Random.Range(0, catDataLength);

        //Create a new pod
        GameObject newPod = Instantiate(pod, spawnPositions[PosIndex], Quaternion.identity);
        newPod.transform.SetParent(CatsParent.transform);

        //Create a new cat and put that inside the pod
        GameObject newCat = Instantiate(catData[getRandomIndex].catPrefab, spawnPositions[PosIndex], Quaternion.identity);
         newCat.transform.SetParent(newPod.transform);

        
        //CatPodInteraction podInteraction = newPod.GetComponent<CatPodInteraction>();
      //  podInteraction.CatOptionsMenu = CatOptionsMenu;

        
        if (newCat.TryGetComponent<DisplayCatInformation>(out var catInfo))
        {
            catInfo.catData = catData[getRandomIndex];
        }

    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateCat();
        }
    }
    */
}
