using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    //https://www.youtube.com/watch?v=eSH9mzcMRqw&ab_channel=GameCodeLibrary
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText, nameText;
    public Image portraitImage;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if(dialogueData == null && !isDialogueActive)
             return;

        if (isDialogueActive)
        {
            //nextline
            nextLine();

        } else
        {
            //start dialogue
            startDialogue();
        }

    }

    void startDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        nameText.SetText(dialogueData.NPCName);
        portraitImage.sprite = dialogueData.NPCImage;

        dialoguePanel.SetActive(true);

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
       
        isTyping=false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            nextLine();
        }
    }

    void nextLine()
    {
        if (isTyping)
        { //skip typing animation and show the entire line
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;

        } else if (dialogueIndex++ < dialogueData.dialogueLines.Length) //if another line, type the next line
        {
            StartCoroutine(TypeLine());
        } else
        {
            //end dialogue
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
    }
}
