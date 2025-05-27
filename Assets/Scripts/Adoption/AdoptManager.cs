using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AdoptManager : MonoBehaviour
{

    [SerializeField] private GameObject adoptionMenuPrefab;
    public float adoptionChance = 0;
  

    public void OpenAdoptionMenu()
    {
        Debug.Log("open adoption menu!");
        PlayerController.Instance.catSelected = gameObject;
        GameObject canvasObject = GameObject.Find("Canvas - Screen Space");
        if (canvasObject != null)
        {
            Canvas canvas = canvasObject.GetComponent<Canvas>();
            if (canvas != null)
            {
                GameObject newMenu = Instantiate(adoptionMenuPrefab, canvas.transform);
                PassNPCDataIntoMenu(newMenu);
                PassCatDataIntoMenu(newMenu);
                CatAdoptionPrediction(newMenu);
            } else
            {
                Debug.Log("Cannot find canvas");
            }
        }
    }

    private void PassNPCDataIntoMenu(GameObject menu)
    {
        GameObject NPC = PlayerController.Instance.companionNPC;

        string name = NPC.GetComponent<NPC>().dialogueData[0].NPCName;
        Sprite image = NPC.GetComponent<NPC>().dialogueData[0].NPCImage;

        Image npcImage = menu.transform.Find("FrameVisitor/NPCImage").GetComponent<Image>();
        TextMeshProUGUI npcName = menu.transform.Find("FrameVisitor/VistorName").GetComponent<TextMeshProUGUI>();

        npcImage.sprite = image;
        npcName.text = name;
    }

    private void PassCatDataIntoMenu(GameObject menu)
    {

        CatData Cat = gameObject.GetComponent<DisplayCatInformation>().catData;

        string name = Cat.catName;
        Sprite image = Cat.catSprite;

        Image catImage = menu.transform.Find("FrameCat/CatImage").GetComponent<Image>();
        TextMeshProUGUI catName = menu.transform.Find("FrameCat/CatName").GetComponent<TextMeshProUGUI>();

        catImage.sprite = image;
        catName.text = name;
    }

    private void CatAdoptionPrediction(GameObject menu)
    {
        CatData Cat = gameObject.GetComponent<DisplayCatInformation>().catData;
        GameObject NPC = PlayerController.Instance.companionNPC;
        TextMeshProUGUI percentage = menu.transform.Find("Adoption Likelihood/Percentage").GetComponent<TextMeshProUGUI>();

        NPCDialogue npcData = NPC.GetComponent<NPC>().dialogueData[1];

        int matchCount = 0;

        //checks if the keywords match and then counts how many do
        foreach (KeywordType desired in npcData.desiredTraits)
        {
            if (Cat.traits.Contains(desired))
            {
                matchCount++;
            }
        }


        //calculates the adoptability
        float matchRatio = (float)matchCount / npcData.desiredTraits.Count;

        adoptionChance = Mathf.Lerp(30f, 100f, matchRatio);

        percentage.text = adoptionChance.ToString("F1") + "%";
    }

}
