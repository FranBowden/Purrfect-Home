
using UnityEngine;

public class ResetCurrentCats : MonoBehaviour
{
    public TimeManager TimeManager;
    private bool hasResetToday = false;
    /*
    private void Update()
    {
        if (TimeManager.isDayOver && !hasResetToday)
        {
            // Reset cat stats
            foreach (Transform cat in transform)
            {
                CatData catData = cat.GetComponent<DisplayCatInformation>().catData;

                if (catData.hygiene >= 0.4f)
                {
                    catData.hygiene -= 0.4f;
                }
                else
                {
                    catData.hygiene = 0f;
                }

                if (catData.hunger >= 0.4f)
                {
                    catData.hunger -= 0.4f;
                }
                else
                {
                    catData.hunger = 0f;
                }

                catData.GenerateWaste = false;
            }

            hasResetToday = true;
        }

        if (!TimeManager.isDayOver && hasResetToday)
        {
            hasResetToday = false;
        }
    }*/
}
