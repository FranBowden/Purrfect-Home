using UnityEngine;

public class CatList : MonoBehaviour
{
    public CatData[] catData;

    //assigns different values to each cat
    private void Start()
    {
        for (int i = 0; i < catData.Length; i++)
        {
            catData[i].hunger = GenerateRandomNumber();

            catData[i].hygiene = GenerateRandomNumber();
            catData[i].GenerateWaste = false;
        }
    }

    private float GenerateRandomNumber()
    {
        return Random.Range(0f, 0.8f);
    }
}
