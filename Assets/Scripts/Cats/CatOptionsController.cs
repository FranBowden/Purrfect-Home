using UnityEngine;

public class CatOptionsMenuController : MonoBehaviour
{

    public GameObject infoMenu;
    public GameObject foodMenu;
    public GameObject cleanMenu;

    private GameObject[] menus;

    void Start()
    {
        menus = new GameObject[] { infoMenu, foodMenu, cleanMenu };

    }

  
    public void ToggleMenu(GameObject menuToToggle)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(menu == menuToToggle);
        }
    }


    public void ToggleInfoMenu() => ToggleMenu(infoMenu);
    public void ToggleFoodMenu() => ToggleMenu(foodMenu);
    public void ToggleCleanMenu() => ToggleMenu(cleanMenu);
}
