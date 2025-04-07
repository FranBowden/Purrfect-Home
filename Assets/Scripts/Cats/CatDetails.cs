using UnityEngine;

[CreateAssetMenu(fileName = "NewCatDetails", menuName = "Cats")]
public class CatDetails : ScriptableObject
{
    public string CatName;
    public Sprite CatImage;
    public float CatAge;
    [TextArea(3, 10)]
    public string CatDescription;
    [TextArea(2, 6)]
    public string CatHealth;
   
    public char grade;

    
    
    public AudioClip voiceSound;
    public float voicePitch = 1f;
}
