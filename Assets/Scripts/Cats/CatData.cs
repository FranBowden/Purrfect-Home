
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCat", menuName = "Cats")]
public class CatData : ScriptableObject
{
    public string catName;
    public Sprite catSprite;

    public float catAge;
    [TextArea]
    public string catDescription;
    public string catListingDescription;
 
    public List<KeywordType> traits;

    public float hunger;
    public float health;
    public float hygiene;

    public int value;

    public int catPodAssigned;
}
