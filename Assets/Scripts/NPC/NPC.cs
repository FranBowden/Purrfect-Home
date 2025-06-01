using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    //https://www.youtube.com/watch?v=eSH9mzcMRqw&ab_channel=GameCodeLibrary
    //useful dialogue tutorial and reference
    public NPCDialogue[] dialogueData;
    public int dialogueDataIndex = 0;
    private DialogueController dialogueControls;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private NPCBehaviour vistorBehaviour;
    private AudioSource textAudio;
    private WayPointMovement wayPointMovement;
    private NPCGenerator npcGen;
    private void Start()
    {
        dialogueControls = DialogueController.Instance;
        vistorBehaviour = GetComponent<NPCBehaviour>();
        textAudio = GameObject.Find("AudioController").transform.Find("Text Audio").GetComponent<AudioSource>();
        wayPointMovement = GetComponent<WayPointMovement>();
        npcGen = FindAnyObjectByType<NPCGenerator>();
    }
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        //if(dialogueData == null && !isDialogueActive)
          //   return;

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
        dialogueControls.SetNpcInfo(dialogueData[dialogueDataIndex].NPCName, dialogueData[dialogueDataIndex].NPCImage);
        dialogueControls.ShowDialogueUI(true);
       // dialogueControls.ShowContinueUI(false); causes ERROR with dialogue UI displaying
        PauseController.SetPause(true);

        DisplayCurrentLine();
    }
    void NextLine()
    {
        if (isTyping)
        { 
            //skip typing animation and show the entire line
            StopAllCoroutines();
            dialogueControls.SetDialogueText(dialogueData[dialogueDataIndex].dialogueLines[dialogueIndex]);
         //   dialogueControls.ShowContinueUI(true);

            isTyping = false;

        }


        dialogueControls.ClearChoice(); //clear choice

      
        if (dialogueData[dialogueDataIndex].endDialogueLines.Length > dialogueIndex && dialogueData[dialogueDataIndex].endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        foreach (DialogueChoice dialogueChoice in dialogueData[dialogueDataIndex].choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoice(dialogueChoice);
                return;
            }
        }

        if (dialogueIndex++ < dialogueData[dialogueDataIndex].dialogueLines.Length) //if another line, type the next line
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
        textAudio.Play();
        foreach (char letter in dialogueData[dialogueDataIndex].dialogueLines[dialogueIndex])
        {
            dialogueControls.SetDialogueText(dialogueControls.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData[dialogueDataIndex].typingSpeed);
        }
       
        isTyping=false;
        textAudio.Stop();
       // dialogueControls.ShowContinueUI(true);

        if (dialogueData[dialogueDataIndex].autoProgressLines.Length > dialogueIndex && dialogueData[dialogueDataIndex].autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData[dialogueDataIndex].autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueControls.SetDialogueText("");
        dialogueControls.ShowDialogueUI(false);
       // dialogueControls.ShowContinueUI(false);
        PauseController.SetPause(false);
        textAudio.Stop();

    }


    void DisplayChoice(DialogueChoice choice)
    {
      
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int choiceIndex = i;
            int nextIndex = choice.nextDialogueIndex[i];
            dialogueControls.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex, choiceIndex));
        

        }
    }

    void ChooseOption(int nextIndex, int choice)
    {
        ResultOfChoice(choice);
        dialogueIndex = nextIndex;
        dialogueControls.ClearChoice();
        DisplayCurrentLine();
    } 

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());

    }


    void ResultOfChoice(int choice)
    {
        Debug.Log("choice =" + choice);
        switch (choice)
        {
            case 0: //The first choice if yes
               if(dialogueDataIndex == 0)
                {
                    vistorBehaviour.EnterCattery(); //enter cattery
               
                    wayPointMovement.waitTime = 0f;
                } else if (dialogueDataIndex == 1) {
                    vistorBehaviour.FollowPlayer();
                    wayPointMovement.waitTime = 0f;
                }
                break;
            case 1: //Leaves straight away on first choice


                if(vistorBehaviour.enterCattery || vistorBehaviour.followPlayer)
                {
                    vistorBehaviour.LeaveCattery();
                } else
                {
                    vistorBehaviour.LeaveShelter();
                   

                }
                //Create a new npc
                npcGen.CreateNPC(); 

                break;
        }
        wayPointMovement.currentWaypointIndex = 0;

    }
}
