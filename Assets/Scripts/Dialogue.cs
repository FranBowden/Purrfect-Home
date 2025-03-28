using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject diaologueBox;
    [SerializeField] private string dialogueText;
    private GameObject NewDialogueBox;
    private Canvas canvas;
  
    private bool triggered = false;

    private void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
    }
    public void displayDialogue()
    {
        if (canvas != null)
        {
         NewDialogueBox = Instantiate(diaologueBox);
         NewDialogueBox.transform.SetParent(canvas.transform, false);

            TextMeshProUGUI text = NewDialogueBox.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.text = dialogueText;
        }
    }

    public void CloseDiologue()
    {
        Destroy(NewDialogueBox);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && !triggered)
        {
            gameObject.GetComponent<PauseEvents>().onPause();
            displayDialogue();
            triggered = true;
        }
    }

  
}
