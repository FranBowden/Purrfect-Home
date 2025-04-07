using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    //https://www.youtube.com/watch?v=eSH9mzcMRqw&ab_channel=GameCodeLibrary
    //useful dialogue tutorial and reference
    public NPCDialogue dialogueData;

    private DialogueController dialogueControls;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        dialogueControls = DialogueController.Instance;
    }
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
            NextLine();

        } else
        {
            //start dialogue
            StartDialogue();
        }

    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
    
        dialogueControls.SetNpcInfo(dialogueData.NPCName, dialogueData.NPCImage);
        dialogueControls.ShowDialogueUI(true);

        DisplayCurrentLine();
    }
    void NextLine()
    {
        if (isTyping)
        { 
            //skip typing animation and show the entire line
            StopAllCoroutines();
            dialogueControls.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;

        }


        dialogueControls.ClearChoice(); //clear choice

      
        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                //display choices
                DisplayChoice(dialogueChoice);
                return;
            }
        }

        if (dialogueIndex++ < dialogueData.dialogueLines.Length) //if another line, type the next line
        {
            DisplayCurrentLine();
        }
        else
        {
            //end dialogue
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueControls.SetDialogueText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {

            dialogueControls.SetDialogueText(dialogueControls.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
       
        isTyping=false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueControls.SetDialogueText("");
        dialogueControls.ShowDialogueUI(false);
    }


    void DisplayChoice(DialogueChoice choice)
    {
        Debug.Log("Displaying choices");
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndex[i];
            Debug.Log(nextIndex);
            dialogueControls.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
            
        }
    }

    void ChooseOption(int nextIndex)
    {
        Debug.Log("button clicked - choice: " + nextIndex);
        dialogueIndex = nextIndex;
        dialogueControls.ClearChoice();
        DisplayCurrentLine();
    } 

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }
}
