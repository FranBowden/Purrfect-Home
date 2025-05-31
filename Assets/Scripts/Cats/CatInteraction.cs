using UnityEngine;
using UnityEngine.UI;

public class CatPodInteraction : MonoBehaviour, IInteractable
{

    private AdoptManager adoptManager;
    private CatOptionsMenuController catOptionsMenuController;
    public bool isMenuOpened { get; private set; } = false;
    private GameObject CatOptionsMenu;
    private Button exitButton;
 

    private void Awake()
    {
        catOptionsMenuController = FindFirstObjectByType<CatOptionsMenuController>();

    }
    private void Start()
    {
        adoptManager = GetComponent<AdoptManager>();

    }
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        DisplayOptions();
    }

    private void DisplayOptions()
    {
        if (isMenuOpened || PlayerController.Instance == null)
            return;

        if (!PlayerController.Instance.hasCompanionNPC)
        {
            OpenMenu();
        }
        else
        {
            adoptManager.OpenAdoptionMenu();
        }
    }

    void OpenMenu()
    {
        PlayerController.Instance.catViewing = gameObject;

        Debug.Log("Opening Cat Options Menu");
        CatOptionsMenu = GameObject.Find("Canvas - Screen Space").transform.Find("CatOptionsMenu").gameObject; //this references cat options menu by canvas (this is because cat options menu starts off as unticked in the inspector) 
        CatOptionsMenu.SetActive(true); //show the menu
        isMenuOpened = true;
        catOptionsMenuController.ToggleInfoMenu();

        exitButton = CatOptionsMenu.transform.Find("Buttons/ExitBtn").GetComponent<Button>(); //get the exit button from the catsoptionmenu

        exitButton.onClick.AddListener(() => CloseMenu()); //if clicked on the exit button - it closes the menu

        DisplayCatInformation catInfo = gameObject.GetComponent<DisplayCatInformation>(); //this gets the cats displaycatinformation script (since the cat is a child of pod)

        catInfo.DisplayCatUI(CatOptionsMenu); //call the display function


    }
    public void CloseMenu()
    {
        Debug.Log("Closing Cat Options Menu");
        CatOptionsMenu.SetActive(false);
        isMenuOpened = false;
        PlayerController.Instance.catViewing = null;

    

    }

}
