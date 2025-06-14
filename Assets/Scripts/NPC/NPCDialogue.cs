using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string NPCName;
    public Sprite NPCImage;
    public SpriteLibraryAsset NPCSpriteLibraryAsset;

    public List<KeywordType> desiredTraits;

    public string[] dialogueLines;
    public DialogueChoice[] choices;
    public bool[] endDialogueLines;

  
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
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

