using UnityEngine;
[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string NPCName;
    public Sprite NPCImage;
    public string[] dialogueLines;
    public DialogueChoice[] choices;
    public bool[] endDialogueLines;

  
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;



    public AudioClip voiceSound;
    public float voicePitch = 1f;
}

[System.Serializable]

public class DialogueChoice
{
    [Header("Dialogue line where the choices will appear")]
    public int dialogueIndex;

    [Header("Player response options")]
    public string[] choices;

    [Header("Where the choices leads")]
    public int[] nextDialogueIndex;
}
