using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "NewCat", menuName = "Cats")]
public class CatData : ScriptableObject
{
    [System.Serializable]
    public class KeywordSelection
    {
        public KeywordType keyword;
        public bool DoesHave;  
    }

    public string catName;
  

    public Sprite catSprite;

    public float catAge;
    [TextArea]
    public string catDescription;
    public string catListingDescription;
    public List<KeywordSelection> keyWords = new List<KeywordSelection>();
    private void OnEnable()
    {
        keyWords.Clear(); //clear the list

    }
    public float hunger;
    public float health;
    public float hygiene;

    public int value;

    public int catPodAssigned;
}
