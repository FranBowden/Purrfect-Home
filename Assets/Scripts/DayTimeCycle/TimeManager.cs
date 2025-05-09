using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
 //   private float dayDuration = 3f * 10f; //debugging time
    private float dayDuration = 3f * 60f;
    private float timer = 0f;
    public int day = 1;
    private bool gameOver = false;
    public bool isDayOver = false;
    private Canvas canvas;

    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private PolygonCollider2D mapBoundary;
    [SerializeField] private CatComputerData catComputerData;
   
    [SerializeField] private TextMeshProUGUI DayUI; //this is the day textmeshpro
    [SerializeField] private TextMeshProUGUI TimeOfDayUI;
    [SerializeField] private GameObject newDayPrefab; //this is the black canvas that says DAY 1.. DAY 2

    private void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        GameObject newDay = Instantiate(newDayPrefab); //this will display day 1
        newDay.transform.SetParent(canvas.transform, false); //gets assigned to child of canvas so it shows up
    }
    private void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);

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
                    TimeOfDayUI.GetComponent<TextMeshProUGUI>().text = "Morning";

                    break;

                case "Afternoon":
                    TimeOfDayUI.GetComponent<TextMeshProUGUI>().text = "Afternoon";

                    break;

                case "Evening":
                    TimeOfDayUI.GetComponent<TextMeshProUGUI>().text = "Evening";
          
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
        if(day == 7) //once the 7 days are up - the game is over
        {
            gameOver = true;
            Debug.Log("The game is over");
            PauseController.SetPause(true);
            return;
        }

        Debug.Log("Game day has ended!");
        day++; //starts a new day
        catComputerData.RefillCatSuggestions();
        GameObject newDay = Instantiate(newDayPrefab);
        newDay.transform.SetParent(canvas.transform, false);
        TextMeshProUGUI text = newDay.transform.Find("Day").GetComponent<TextMeshProUGUI>();
        text.text = "Day" + day;
   
        timer = -2.5f; //resets the timer [-2.5 to account for the animation time]
        DayUI.text = "Day: " + day.ToString();
        isDayOver = false;
        
        
    }
}
