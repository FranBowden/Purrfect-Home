using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance {  get; private set; }
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText, nameText;
    public Image portraitImage;
    public Transform choiceContainer;
    public GameObject choiceBtnPrefab;
    public GameObject continueUI;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNpcInfo(string npcName, Sprite portrait)
    {
        if (nameText == null) Debug.LogError("nameText is not assigned!");
        if (portraitImage == null) Debug.LogError("portraitImage is not assigned!");

        nameText.text = npcName;
        portraitImage.sprite = portrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }

    public void ShowContinueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void ClearChoice()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        
        GameObject choiceBtn = Instantiate(choiceBtnPrefab, choiceContainer);
      
            choiceBtn.GetComponentInChildren<TMP_Text>().text = choiceText;
            choiceBtn.GetComponent<Button>().onClick.AddListener(onClick);
      
      

    }

}
