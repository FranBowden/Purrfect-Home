using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Cat : MonoBehaviour, IInteractable
{

    public bool catMenuOn { get; private set; }
    public string catID { get; private set; }
    [SerializeField] GameObject catMenuPrefab;

    [Header("Cat Details:")]
    [SerializeField] private string catName;
    [SerializeField] private string catDescription;
    [SerializeField] private string catAge;
    
    private GameObject newCatMenu;
    private Canvas canvas;
    public bool isCatMenuOpened;

    void Start()
    {
    
        canvas = FindAnyObjectByType<Canvas>();
        isCatMenuOpened = false;
    }
    public bool CanInteract()
    {
        return !catMenuOn;

    }

    public void Interact()
    {
        if (!CanInteract()) return;
        displayDesciptionCat();

    }

    private void displayDesciptionCat()
    {
        if (canvas != null && GameObject.Find("CatMenu(Clone)") == null) //check there is no duplicates
        {
            newCatMenu = Instantiate(catMenuPrefab);
            newCatMenu.transform.SetParent(canvas.transform, false);


            TextMeshProUGUI CatName = newCatMenu.transform.Find("CatName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatDesc = newCatMenu.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatID = newCatMenu.transform.Find("CatID").GetComponent<TextMeshProUGUI>();
            Image CatImage = newCatMenu.transform.Find("CatImage").GetComponent<Image>();
            SpriteRenderer cat = gameObject.GetComponent<SpriteRenderer>();
           
          
            if (CatName != null)
            {
                CatName.text = catName;

                Debug.Log("Updated cat name");
            }

            if(CatDesc != null)
            {
                CatDesc.text = catDescription;
                Debug.Log("Updated cat description");
            }

            if (CatID != null)
            {
                CatID.text = catID;
                Debug.Log("Updated catID");
            }


            if (cat != null && CatImage != null)
            {
                CatImage.sprite = cat.sprite;
                Debug.Log("Updated cat image");
            }

            
                
        
        }
  
       
    }

}
