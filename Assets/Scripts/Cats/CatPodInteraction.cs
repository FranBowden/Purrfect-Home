using UnityEngine;
using UnityEngine.UI;

public class CatPodInteraction : MonoBehaviour, IInteractable
{
    public bool isMenuOpened { get; private set; } = false;
    private GameObject CatOptionsMenu;
    private Button exitButton;
    private GameObject infoMenu;
    

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
        if (!isMenuOpened) OpenMenu();
        
    }

    void OpenMenu()
    {
        Debug.Log("Opening Cat Options Menu");
        CatOptionsMenu = GameObject.Find("Canvas - Screen Space").transform.Find("CatOptionsMenu").gameObject; //this references cat options menu by canvas (this is because cat options menu starts off as unticked in the inspector) 
        CatOptionsMenu.SetActive(true); //show the menu
        isMenuOpened = true;

        exitButton = CatOptionsMenu.transform.Find("Buttons/ExitBtn").GetComponent<Button>(); //get the exit button from the catsoptionmenu
        infoMenu = CatOptionsMenu.transform.Find("CatInformation").gameObject; //then also get the info menu from catoptionsmenu

        exitButton.onClick.AddListener(() => CloseMenu()); //if clicked on the exit button - it closes the menu

        DisplayCatInformation catInfo = transform.GetChild(0).gameObject.GetComponent<DisplayCatInformation>(); //this gets the cats displaycatinformation script (since the cat is a child of pod)

        catInfo.displayCatInfo(infoMenu); //call the display function

    }
    public void CloseMenu()
    {
        Debug.Log("Closing Cat Options Menu");
        CatOptionsMenu.SetActive(false);
        isMenuOpened = false;
    }

}
