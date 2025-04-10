using UnityEngine;

[CreateAssetMenu(fileName = "NewCat", menuName = "Cats")]
public class CatData : ScriptableObject
{
    public string catName;
    public GameObject catPrefab;
    public float catAge;
    [TextArea]
    public string catDescription;
}
