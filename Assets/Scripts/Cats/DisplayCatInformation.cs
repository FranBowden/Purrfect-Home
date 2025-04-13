using System.Linq;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCatInformation : MonoBehaviour
{
    public CatData catData;
    public void displayCatInfo(GameObject infoMenu)
    {

            TextMeshProUGUI CatName = infoMenu.transform.Find("CatName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatDesc = infoMenu.transform.Find("CatDescription").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatHealth = infoMenu.transform.Find("CatHealth").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatHygiene = infoMenu.transform.Find("CatHygiene").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatHunger = infoMenu.transform.Find("CatHunger").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CatGrade = infoMenu.transform.Find("CatGrade").GetComponent<TextMeshProUGUI>();


            Image CatImage = infoMenu.transform.Find("CatImage").GetComponent<Image>();


            if (CatName != null || CatDesc != null || CatImage != null)
            {
                CatName.text = catData.catName;
                CatDesc.text = catData.catDescription;
                CatImage.sprite = catData.catPrefab.GetComponent<SpriteRenderer>().sprite;
                CatHealth.text = "Health: " + catData.health;
                CatHygiene.text = "Hygiene: " + catData.hygiene;
                CatHunger.text = "Hunger: " + catData.hunger;
                CatGrade.text = CatGrade.text;

            }
        
    }
}
