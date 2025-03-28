using UnityEngine;
[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    //https://www.youtube.com/watch?v=eSH9mzcMRqw&ab_channel=GameCodeLibrary
    public string NPCName;
    public Sprite NPCImage;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;


}
