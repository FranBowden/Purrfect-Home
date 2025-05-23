using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    private float dayDuration = 3f * 2f; //debugging time
  //  private float dayDuration = 3f * 60f;
    private float timer = 0f;
    private bool gameOver = false;
    private bool hasStartedMusic = false;
   
    public bool isDayOver = false;
    public int day = 1;

    [SerializeField] private CatComputerData catComputerData;
    [SerializeField] private TextMeshProUGUI DayUI; //this is the day textmeshpro
    [SerializeField] private TextMeshProUGUI TimeOfDayUI;
    [SerializeField] private GameObject newDayPrefab; //this is the black canvas that says DAY 1.. DAY 2
    [SerializeField] private GameObject EndDayButton;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Canvas canvas;
    
    private void Start()
    {
        GameObject newDay = Instantiate(newDayPrefab); //this will display day 1
        newDay.transform.SetParent(canvas.transform, false); //gets assigned to child of canvas so it shows up

    }

    private void Update()
    {
        timer += Time.deltaTime;
//        Debug.Log(timer);

        if (timer >= dayDuration && !isDayOver)
        {
            isDayOver = true;
            //  EndDay();
            EndDayButton.SetActive(true); //show button to end day


        }

        if (timer >= 2.5f && !hasStartedMusic)
        {
            hasStartedMusic = true;

            backgroundMusic.loop = true; 
            backgroundMusic.Play();
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
        float morningEnd = dayDuration * 0.45f;
        float afternoonEnd = dayDuration * 0.98f;

        if (timer < morningEnd)
        {
            return "Morning";
        }
        else if (timer < afternoonEnd)
        {
            return "Afternoon";
        }
        else
        {
            return "Evening";
        }
    }

    public void EndDay()
    {
        EndDayButton.SetActive(false);
        player.GetComponent<Transform>().localPosition = new Vector2(0f, -7.5f);


        if (hasStartedMusic)
        {
            backgroundMusic.Stop();
            hasStartedMusic = false;
        }


        if (day == 7) //once the 7 days are up - the game is over
        {
            gameOver = true;
            Debug.Log("The game is over");
            PauseController.SetPause(true);
            return;
        }

        Debug.Log("Game day has ended!");
        day++; //starts a new day

       // catComputerData.RefillCatSuggestions();
        GameObject newDay = Instantiate(newDayPrefab);
        newDay.transform.SetParent(canvas.transform, false);
        TextMeshProUGUI text = newDay.transform.Find("Day").GetComponent<TextMeshProUGUI>();
        text.text = "Day " + day;
   
        timer = -2.5f; //resets the timer [-2.5 to account for the animation time]
        DayUI.text = "Day: " + day.ToString();
        isDayOver = false;

       //player.GetComponent<Animator>().SetFloat("LastInputX", 0);
        //player.GetComponent<Animator>().SetFloat("LastInputY", 1);


    }
}
