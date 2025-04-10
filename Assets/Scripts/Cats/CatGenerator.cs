using UnityEngine;

public class CatGenerator : MonoBehaviour
{

    [SerializeField] private GameObject CatsParent;
    [SerializeField] private CatData[] catData;
    private Vector3 spawnPoint;
    private int[] previouslyUsedCatIndex;
    private GameObject[] CatsCurrentlyInShelter;

    private int catDataLength;

    private void Start()
    {
        
        catDataLength = catData.Length;
        spawnPoint = new Vector3 (0, 0, 0);
    }


    public void CreateCat()
    {
        Debug.Log("create cat");
        int getRandomIndex = Random.Range(0, catDataLength);

        // catData[getRandomIndex];

        GameObject newCat = Instantiate(catData[getRandomIndex].catPrefab, spawnPoint, Quaternion.identity);
        newCat.transform.SetParent(CatsParent.transform); //makes the new cat a child of the CATs folder

        CatInteraction catInteraction = newCat.GetComponent<CatInteraction>();
        if (catInteraction != null)
        {
            catInteraction.catData = catData[getRandomIndex];
        }
        else
        {
            Debug.LogWarning("CatInteraction script not found on the cat prefab!");
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
