using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static HealthBarController;

public class DisplayCatInformation : MonoBehaviour
{
    public CatData catData;
    private HealthBarController healthBarController;

    private void Start()
    {
        healthBarController = FindFirstObjectByType<HealthBarController>();
    }
    public void DisplayCatInfo(GameObject infoMenu)
    {

            TextMeshProUGUI CatName = infoMenu.transform.Find("CatName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatDesc = infoMenu.transform.Find("CatDescription").GetComponent<TextMeshProUGUI>();
     
            Image CatImage = infoMenu.transform.Find("CatImage").GetComponent<Image>();


            if (CatName != null || CatDesc != null || CatImage != null && healthBarController != null)
            {
                CatName.text = catData.catName;
                CatDesc.text = catData.catDescription;
                CatImage.sprite = catData.catSprite;
                healthBarController.CalculateBar(catData.health, BarType.EnergyBar);
                healthBarController.CalculateBar(catData.hunger, BarType.HungerBar);
                healthBarController.CalculateBar(catData.hygiene, BarType.HygieneBar);
        }

    }

}
