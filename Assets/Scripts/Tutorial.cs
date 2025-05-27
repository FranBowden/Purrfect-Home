using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] string[] tutorialDialogue;
    [SerializeField] GameObject tutorialDialogueUI;
    [SerializeField] GameObject canvas;
    public int dialogueIndex = 0;

    private void Start()
    {
        StartCoroutine(PlayTutorialDialogue());
    }

    private void SetText()
    {
        GameObject UI = Instantiate(tutorialDialogueUI);
        UI.transform.SetParent(canvas.transform, false);  

        TextMeshProUGUI text = UI.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null && dialogueIndex < tutorialDialogue.Length)
        {
            text.text = tutorialDialogue[dialogueIndex];
        }


    }

    IEnumerator PlayTutorialDialogue()
    {
        yield return new WaitForSeconds(3f);  

        while (dialogueIndex < tutorialDialogue.Length)
        {
           SetText();
            yield return new WaitForSeconds(2f);  

    
        
            dialogueIndex++;
        }
    }
}
