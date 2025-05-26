using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private CatComputerData catComputerData;
    [SerializeField] private TextMeshProUGUI DayUI; //this is the day textmeshpro
    [SerializeField] private TextMeshProUGUI TimeOfDayUI;
    [SerializeField] private GameObject newDayPrefab; //this is the black canvas that says DAY 1.. DAY 2
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Canvas canvas;
    [SerializeField] private NPCGenerator NPCGenerator;
    [SerializeField] private GameObject NPCParent;
    [SerializeField] private GameObject GameOverMenu;

    private bool hasStartedMusic = false;
    public bool isDayOver = false;
    public int day = 1;
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
        if (day < 5)
        {
            StartNewDay();
        } else
        {
            CheckIfGameIsOver();
        }
    }

    private void HandleDayEndState()
    {
        if(AdoptionStats.Instance.CatsAdoptedToday == 0)
        {
            AdoptionShelterReputation.Instance.RemovePoints(100); //100 points get removed for not adopting any cats...
        }
        isDayOver = true;
        NPCGenerator.ResetShelter();
        AdoptionStats.Instance.CatsShelteredToday = 0;
        AdoptionStats.Instance.CatsAdoptedToday = 0;
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
            Debug.Log("The game is over");
            PauseController.SetPause(true);
            GameOver();
            return;
        
    }

    private void StartNewDay()
    {
        Debug.Log("Game day has ended!");
        day++;

        catComputerData.RefillCatSuggestions();
        ShowNewDayUI();
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

    private void GameOver()
    {
        var rep = AdoptionShelterReputation.Instance;
        var stats = AdoptionStats.Instance;

        GameOverMenu.SetActive(true);
        string statsMessage = "You helped " + stats.numCatsAdopted + " cats find a home! " +
            "Total points: " + rep.currentPoints;

        //display stars
        rep.DisplayStars(rep.starRating, GameOverMenu.transform.Find("Stars").GetComponent<Image>());

        TextMeshProUGUI friendlyMessageUI = GameOverMenu.transform.Find("FriendlyMessage").GetComponent<TextMeshProUGUI>();
        friendlyMessageUI.text = GetFriendlyMessage(rep.starRating);

        TextMeshProUGUI statsMessageUI = GameOverMenu.transform.Find("Stats").GetComponent<TextMeshProUGUI>();

        statsMessageUI.text = statsMessage;


    }

    private string GetFriendlyMessage(int starRating)
    {
        if (starRating == 5)
            return "Purrfect job!";
        else if (starRating >= 3)
            return "Good try!";
        else if (starRating > 0)
            return "Keep trying!";
        else
            return "Oops! Try again";
    }
}
