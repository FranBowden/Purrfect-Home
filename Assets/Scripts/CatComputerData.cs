using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatComputerData : MonoBehaviour
{
    public CatData[] catData;
    [SerializeField] GameObject prefabCatListingItem;
    [SerializeField] Transform prefabCatListing;


    private GameObject[] CatListing;
    private void Start()
    {
        CatListing = new GameObject[catData.Length];
        refillCatSuggestions(3);


        for(int i = 0; i < CatListing.Length; i++)
        {
            
            if (CatListing[i].transform.Find("Button").TryGetComponent<Button>(out var btn))
            {
                int index = i; 
                btn.onClick.AddListener(() => catAccepted(index)); //listens to which button has been pressed
            }
        }
    }
   private void refillCatSuggestions(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject newCatListing = Instantiate(prefabCatListingItem);
            newCatListing.transform.SetParent(prefabCatListing, false);

            CatListing[i] = newCatListing;

            setCatDataToList(i);
        }
    }

    private void setCatDataToList(int index)
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


    private void catAccepted(int index)
    {
        Debug.Log("You accepted " + CatListing[index].transform.Find("Cat Information/CatName").GetComponent<TextMeshProUGUI>().text); //shows the name of the selected cat

        Destroy(CatListing[index]); //removes cat from the list
    }
}
