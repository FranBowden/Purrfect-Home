using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{

    [System.Serializable]
    public class KeywordSelection
    {
        public KeywordType keyword;
        public bool isWanted;
    }

    public string NPCName;
    public Sprite NPCImage;
    public SpriteLibraryAsset NPCSpriteLibraryAsset;

    
    public List<KeywordSelection> keyWords = new List<KeywordSelection>();
    private void OnEnable()
    {
        keyWords.Clear(); //clear the list
    }

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

