using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static HealthBarController;


public class CatOptionsMenuController : MonoBehaviour
{
    public GameObject infoMenu;
    public GameObject foodMenu;
    public GameObject cleanMenu;

    [Header("Getting Health Data")]

    public GameObject[] info;
    public GameObject[] food;
    public GameObject[] clean;


    public bool hasDataTransferred = false;

    private GameObject[] menus;
    private HealthBarController HealthBarController;

    public bool cleaningMenuShowing = false;
    void Start()
    {
        menus = new GameObject[] { infoMenu, foodMenu, cleanMenu };
        HealthBarController = GetComponent<HealthBarController>();
    }
  

    public void ToggleMenu(GameObject menuToToggle)
    {
        GameObject catViewing = PlayerController.Instance.catViewing;
        CatData catData = catViewing.GetComponent<DisplayCatInformation>().catData;



        foreach (GameObject menu in menus)
        {
            menu.SetActive(menu == menuToToggle);

            if (menu == infoMenu)
            {
                gameObject.GetComponent<CatEnergy>().CalculateEnergy();

                HealthBarController.ReAssignBarMap(info);
                HealthBarController.CalculateBar(catData.hunger, BarType.HungerBar);
                HealthBarController.CalculateBar(catData.health, BarType.EnergyBar);
                HealthBarController.CalculateBar(catData.hygiene, BarType.HygieneBar);
                cleaningMenuShowing = false;

            }
            else if (menu == foodMenu)
            {
            
                HealthBarController.ReAssignBarMap(food);
                HealthBarController.CalculateBar(catData.hunger, BarType.HungerBar);
                cleaningMenuShowing = false;

            }
            else if (menu == cleanMenu)
            {
                cleaningMenuShowing = true;
                HealthBarController.ReAssignBarMap(clean);
                HealthBarController.CalculateBar(catData.hygiene, BarType.HygieneBar);


            }
        }
    }

    public void ToggleInfoMenu() => ToggleMenu(infoMenu);
    public void ToggleFoodMenu() => ToggleMenu(foodMenu);
    public void ToggleCleanMenu() => ToggleMenu(cleanMenu);
}
