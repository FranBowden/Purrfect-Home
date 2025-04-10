using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CatInteraction : MonoBehaviour, IInteractable
{

    public bool catMenuOn { get; private set; }
    public string catID { get; private set; }
    private GameObject catMenu;
    private GameObject newCatMenu;
    private Canvas canvas;
    public bool isCatMenuOpened;

    public CatData catData;

    void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        catMenu = canvas.GetComponentsInChildren<Transform>(true)
      .FirstOrDefault(t => t.name == "CatMenu")?.gameObject;
        isCatMenuOpened = false;
    }
    public bool CanInteract()
    {
        return !catMenuOn;

    }

    public void Interact()
    {
        if (!CanInteract()) return;
        DisplayCatDataMenu();

    }

    void OpenMenu()
    {
        Debug.Log("Open Menu");
        catMenu.SetActive(true);
        isCatMenuOpened = true;
    }

    void CloseMenu()
    {
        Debug.Log("Close Menu");
        catMenu.SetActive(false);
        isCatMenuOpened = false;

    }

    private void DisplayCatDataMenu()
    {

       

        if (canvas != null && catMenu != null)
        {

            if (!isCatMenuOpened)
            {
                OpenMenu();


                TextMeshProUGUI CatName = catMenu.transform.Find("CatName").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI CatDesc = catMenu.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
                Image CatImage = catMenu.transform.Find("CatImage").GetComponent<Image>();


                if (CatName != null && CatDesc != null && CatImage != null)
                {
                    CatName.text = catData.catName;
                    CatDesc.text = catData.catDescription;
                    CatImage.sprite = catData.catPrefab.GetComponent<SpriteRenderer>().sprite;

                }
                else
                {
                    Debug.Log("error with assigning cat information");
                }

            } else
            {
                CloseMenu();
            }
        
        } 
        
        else
        {
            Debug.Log("Cannot open menu due to missing information");
        }
            
    }

}
