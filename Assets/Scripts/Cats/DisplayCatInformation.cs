using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static HealthBarController;

public class DisplayCatInformation : MonoBehaviour
{
    public CatData catData;
    private HealthBarController healthBarController;

   // private AudioSource meow;

    private void Start()
    {
        healthBarController = FindFirstObjectByType<HealthBarController>();
      //  GameObject audioController = GameObject.Find("AudioController");
   //     meow = audioController.transform.Find("Meow1").GetComponent<AudioSource>();
    }

    public void DisplayCatUI(GameObject menu)
    {
        TextMeshProUGUI catName = menu.transform.Find("CatInformation/CatName")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI catDesc = menu.transform.Find("CatInformation/CatDescription")?.GetComponent<TextMeshProUGUI>();
        Image CatInfoCatImage = menu.transform.Find("CatInformation/CatImage")?.GetComponent<Image>();
        TextMeshProUGUI catValue = menu.transform.Find("CatInformation/CatValue")?.GetComponent<TextMeshProUGUI>();

        //because UI is hidden:
        Image CatFoodCatImage = menu.GetComponentsInChildren<Image>(true)
        .FirstOrDefault(img => img.name == "CatImage" && img.transform.parent.name == "CatFood");

        Image CatCleanCatImage = menu.GetComponentsInChildren<Image>(true)
      .FirstOrDefault(img => img.name == "CatImage" && img.transform.parent.name == "CatClean");

        if (catName != null)
            catName.text = catData.catName;

        if (catDesc != null)
            catDesc.text = catData.catDescription;
        
        if (catValue != null)
            catValue.text = "Value: $" + catData.value.ToString();

        if (CatInfoCatImage != null)
            CatInfoCatImage.sprite = catData.catSprite;
            CatFoodCatImage.sprite = catData.catSprite;
            CatCleanCatImage.sprite = catData.catSprite;

        

    }


}
