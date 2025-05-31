using System.Collections.Generic;
using TMPro;
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
                gameObject.GetComponent<CatEnergy>().CalculateEnergy(); //calculate energy/health first

                HealthBarController.ReAssignBarMap(info);
                HealthBarController.CalculateBar(catData.hunger, BarType.HungerBar);
                HealthBarController.CalculateBar(catData.health, BarType.EnergyBar);
                HealthBarController.CalculateBar(catData.hygiene, BarType.HygieneBar);
         

            }
            else if (menu == foodMenu)
            {
            
                HealthBarController.ReAssignBarMap(food);
                HealthBarController.CalculateBar(catData.hunger, BarType.HungerBar);
         
               // FoodStock.text = "Stock: " + PlayerController.Instance.CatFood.ToString();


            }
            else if (menu == cleanMenu)
            {
         
                HealthBarController.ReAssignBarMap(clean);
                HealthBarController.CalculateBar(catData.hygiene, BarType.HygieneBar);
                //     CleanStock.text = "Stock: " +  PlayerController.Instance.CatLitter.ToString();

             



            }
        }
    }

    public void ToggleInfoMenu() => ToggleMenu(infoMenu);
    public void ToggleFoodMenu() => ToggleMenu(foodMenu);
    public void ToggleCleanMenu() => ToggleMenu(cleanMenu);
}
