using UnityEngine;

public class CatOptionsMenuController : MonoBehaviour
{
    public bool isCatMenuOpened;

    public GameObject infoMenu;
    public GameObject foodMenu;
    public GameObject cleanMenu;

    private GameObject[] menus;

    void Start()
    {
        menus = new GameObject[] {infoMenu, foodMenu, cleanMenu};
        isCatMenuOpened = false;
}

public void ToggleMenu(GameObject menuToToggle)
    {
        foreach (GameObject menu in menus)
        {
            if (menu == menuToToggle)
            {
                menu.SetActive(!menu.activeSelf);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }



    public void ToggleInfoMenu() => ToggleMenu(infoMenu);
    public void ToggleFoodMenu() => ToggleMenu(foodMenu);
    public void ToggleCleanMenu() => ToggleMenu(cleanMenu);
}
