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
        CatOptionsMenu = GameObject.Find("Canvas - Screen Space").transform.Find("CatOptionsMenu").gameObject;
        CatOptionsMenu.SetActive(true);
        isMenuOpened = true;

        exitButton = CatOptionsMenu.transform.Find("ButtonsPanel/ExitButton").GetComponent<Button>();
        infoMenu = CatOptionsMenu.transform.Find("Info").gameObject;

        exitButton.onClick.AddListener(() => CloseMenu());

        DisplayCatInformation catInfo = transform.GetChild(0).gameObject.GetComponent<DisplayCatInformation>();

        catInfo.displayCatInfo(infoMenu);

    }
    public void CloseMenu()
    {
        Debug.Log("Closing Cat Options Menu");
        CatOptionsMenu.SetActive(false);
        isMenuOpened = false;
    }

}
