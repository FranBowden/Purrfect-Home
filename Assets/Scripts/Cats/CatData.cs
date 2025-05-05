using System.Collections.Generic;
using UnityEngine;

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
    public GameObject catPrefab;
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

    public char OverallGrade;
}
