using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdoptManager : MonoBehaviour
{

    [SerializeField] private GameObject adoptionMenuPrefab;
  

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
        Sprite image = Cat.catPrefab.GetComponent<SpriteRenderer>().sprite;

        Image catImage = menu.transform.Find("FrameCat/CatImage").GetComponent<Image>();
        TextMeshProUGUI catName = menu.transform.Find("FrameCat/CatName").GetComponent<TextMeshProUGUI>();

        catImage.sprite = image;
        catName.text = name;
    }



}
