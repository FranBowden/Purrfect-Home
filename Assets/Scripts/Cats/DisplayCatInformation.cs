using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static HealthBarController;

public class DisplayCatInformation : MonoBehaviour
{
    public CatData catData;
    private HealthBarController healthBarController;
    private CatOptionsMenuController CatOptionsTabs;

    private void Start()
    {
        healthBarController = FindFirstObjectByType<HealthBarController>();
        CatOptionsTabs = FindFirstObjectByType<CatOptionsMenuController>();
    }

    public void DisplayCatUI(GameObject menu)
    {
        TextMeshProUGUI catName = menu.transform.Find("CatInformation/CatName")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catDesc = menu.transform.Find("CatInformation/CatDescription")?.GetComponent<TextMeshProUGUI>();
        Image CatInfoCatImage = menu.transform.Find("CatInformation/CatImage")?.GetComponent<Image>();

        //because UI is hidden:
        Image CatFoodCatImage = menu.GetComponentsInChildren<Image>(true)
        .FirstOrDefault(img => img.name == "CatImage" && img.transform.parent.name == "CatFood");

        if (catName != null)
            catName.text = catData.catName;

        if (catDesc != null)
            catDesc.text = catData.catDescription;

        if (CatInfoCatImage != null)
            CatInfoCatImage.sprite = catData.catSprite;
            CatFoodCatImage.sprite = catData.catSprite;

        if (healthBarController != null)
        {
            if (healthBarController != null)
            {
              /*
                healthBarController.CalculateBar(catData.hunger, BarType.HungerBar);
                healthBarController.CalculateBar(catData.health, BarType.EnergyBar);
                healthBarController.CalculateBar(catData.hygiene, BarType.HygieneBar);

                FoodMechanics.GetCatData(catData); //passes the data to food mechanics*/

            }
        }
    }


}
