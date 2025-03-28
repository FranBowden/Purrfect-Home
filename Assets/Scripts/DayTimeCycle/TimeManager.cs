using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float dayDuration = 10f * 60f; //12 minutes in seconds
    private float timer = 0f;
    private int day = 1;
    private bool gameOver = false;
    public bool isDayOver = false;
    private Canvas canvas;
    private Transform player;
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private PolygonCollider2D mapBoundary;


    [SerializeField] private TextMeshProUGUI DayUI; //this is the day textmeshpro
    [SerializeField] private GameObject newDayPrefab; //this is the black canvas that says DAY 1.. DAY 2

    private void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        player = GameObject.FindWithTag("Player").transform;
       

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
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Morning";

                    break;

                case "Afternoon":
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Afternoon";

                    break;

                case "Evening":
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Evening";
                  
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
            return;
        }

        Debug.Log("Game day has ended!");
        day++; //starts a new day
       
        GameObject newDay = Instantiate(newDayPrefab);
        newDay.transform.SetParent(canvas.transform, false);
        TextMeshProUGUI text = newDay.transform.Find("Day").GetComponent<TextMeshProUGUI>();
        text.text = "Day" + day;
      //  confiner.BoundingShape2D = mapBoundary;


        timer = -2.5f; //resets the timer [-2.5 to account for the animation time]
        DayUI.text = "Day: " + day.ToString();
        isDayOver = false;
        
        
    }
}
