using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float dayDuration = 14f * 60f; //14 minutes in seconds

    private float timer = 0f;
    private bool isMorning = true;
    private bool isAfternoon = false;
    private bool isEvening = false;

    private bool isDayOver = false;

    private void Update()
    {
        timer += Time.deltaTime;


        if (timer >= dayDuration && !isDayOver)
        {
            isDayOver = true;
            EndDay();
        }

        if (timer < dayDuration)
        {
            string timeOfDay = GetTimeOfDay();
            switch (timeOfDay)
            {
                case "Morning":
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Morning";

                    Debug.Log("Morning");
                    break;

                case "Afternoon":
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Afternoon";

                    Debug.Log("Afternoon");
                    break;

                case "Evening":
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Evening";
                    Debug.Log("Evening");
                    break;

                default:
                    Debug.Log("Invalid Time");
                    break;
            }
        }
    }

    string GetTimeOfDay()
    {
        if (timer < dayDuration / 3f)
        {
            return "Morning"; 
        }
        else if (timer < (dayDuration / 3f) * 2)
        {
            return "Afternoon"; 
        }
        else
        {
            return "Evening"; 
        }
    }

    void EndDay()
    {
        Debug.Log("Game day has ended!");
        timer = 0f;
        isDayOver = false;
    }

}
