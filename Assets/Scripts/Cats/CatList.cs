using UnityEngine;

public class CatList : MonoBehaviour
{
    public CatData[] catData;

    //assigns different values to each cat
    private void Start()
    {
        for (int i = 0; i < catData.Length; i++)
        {
            catData[i].hunger = GenerateRandomNumber(0, 0.8f);
            catData[i].hygiene = GenerateRandomNumber(0, 0.8f);
            float randomValue = GenerateRandomNumber(70f, 150f);
            catData[i].value = Mathf.RoundToInt(randomValue / 10f) * 10; //round to the nearest 10

            catData[i].GenerateWaste = false;
            
        }
    }

    private float GenerateRandomNumber(float min, float max)
    {
        return Random.Range(min, max);
    }


}
