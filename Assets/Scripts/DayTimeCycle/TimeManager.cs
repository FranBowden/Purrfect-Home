using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

  //  private float dayDuration = 3f * 2f; //debugging time
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
    [SerializeField] private NPCGenerator NPCGenerator;
    [SerializeField] private GameObject NPCParent;
    private void Start()
    {
        GameObject newDay = Instantiate(newDayPrefab); //this will display day 1
        newDay.transform.SetParent(canvas.transform, false); //gets assigned to child of canvas so it shows up
        TimeOfDayUI.GetComponent<TextMeshProUGUI>().text = "Morning";
    }

    public void EndDay()
    {
        HandleDayEndState();
        ResetPlayerPosition();
        StopBackgroundMusicIfPlaying();
        CheckIfGameIsOver();
        StartNewDay();
    }

    private void HandleDayEndState()
    {
        isDayOver = true;
        EndDayButton.SetActive(false);
        NPCGenerator.letVisitorsInside = false;

        //destroy all NPCs
        DestroyAllNPCs();

    }

    private void ResetPlayerPosition()
    {
        player.transform.localPosition = new Vector2(0f, -7.5f);
    }

    private void StopBackgroundMusicIfPlaying()
    {
        if (hasStartedMusic)
        {
            backgroundMusic.Stop();
            hasStartedMusic = false;
        }
    }

    private void CheckIfGameIsOver()
    {
        if (day == 7)
        {
            gameOver = true;
            Debug.Log("The game is over");
            PauseController.SetPause(true);
            return;
        }
    }

    private void StartNewDay()
    {
        Debug.Log("Game day has ended!");
        day++;

        catComputerData.RefillCatSuggestions();
        ShowNewDayUI();
        timer = -2.5f;
        DayUI.text = "Day: " + day.ToString();
        isDayOver = false;
    }

    private void ShowNewDayUI()
    {
        GameObject newDay = Instantiate(newDayPrefab);
        newDay.transform.SetParent(canvas.transform, false);
        TextMeshProUGUI text = newDay.transform.Find("Day").GetComponent<TextMeshProUGUI>();
        text.text = "Day " + day;
    }

    private void DestroyAllNPCs()
    {
        foreach (Transform child in NPCParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
