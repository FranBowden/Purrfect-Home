using System;
using TMPro;
using UnityEngine;
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
    [SerializeField] private TextMeshProUGUI Task;
    [SerializeField] private GameObject newvisitorpopup;

    private bool hasStartedMusic = false;
    public bool isDayOver = false;
    public int day = 1;


    private string[] funFacts = new string[]
{
    "In 2024, 25% of households in the UK have at least one cat",
    "Approximately 150,000 cats enter UK shelters every year",
    "24% of cats were adopted from a UK rehoming charity",
    "Cats Protection rehomes over 30,000 cats annually",
     "Younger people age 18 - 34 are more likely to buy a cat than adopt",
};
    private void Start()
    {
        GameObject newDay = Instantiate(newDayPrefab); //this will display day 1
        newDay.transform.SetParent(canvas.transform, false); //gets assigned to child of canvas so it shows up
        TimeOfDayUI.GetComponent<TextMeshProUGUI>().text = "Morning";

        newDay.transform.Find("FunFact").GetComponent<TextMeshProUGUI>().text = funFacts[day - 1];

    }

    public void EndDay()
    {
     

        HandleDayEndState();
        ResetPlayerPosition();
       // StopBackgroundMusicIfPlaying();

        if (day < 5)
        {
            StartNewDay();
        }
        else
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
        newvisitorpopup.SetActive(false);
        isDayOver = true;
        NPCGenerator.ResetShelter();
        AdoptionStats.Instance.CatsShelteredToday = 0;
        AdoptionStats.Instance.CatsAdoptedToday = 0;
        DestroyAllNPCs();
    }

    private void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.localPosition = new Vector2(0f, -7.5f);
        }
        else
        {
            Debug.LogWarning("Player is missing");
        }
    }

    /*
    private void StopBackgroundMusicIfPlaying()
    {
        if (hasStartedMusic)
        {
            backgroundMusic.Stop();
            hasStartedMusic = false;
        }
    }*/

    private void CheckIfGameIsOver()
    {
            Debug.Log("The game is over");
            PauseController.SetPause(true);
            GameOver();
            return;
        
    }

    private void StartNewDay()
    {
        Task.text = "Task: Accept a cat from the computer";

        //Debug.Log("Game day has ended!");
        day++;

        catComputerData.ClearCatListings();
        catComputerData.RefillCatSuggestions();
     
        
        ShowNewDayUI();
        DayUI.text = "Day: " + day.ToString();
        isDayOver = false;
    }

    private void ShowNewDayUI()
    {
        if (canvas == null || newDayPrefab == null)
        {
            Debug.LogWarning("Missing canvas or newDayPrefab.");
            return;
        }

        GameObject newDay = Instantiate(newDayPrefab);
        newDay.transform.SetParent(canvas.transform, false);
        newDay.transform.Find("FunFact").GetComponent<TextMeshProUGUI>().text = funFacts[day - 1];

        var dayTextTransform = newDay.transform.Find("Day");
        if (dayTextTransform == null)
        {
            Debug.LogWarning("NewDay prefab missing 'Day' child object.");
            return;
        }

        TextMeshProUGUI text = dayTextTransform.GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogWarning("'Day' child missing TextMeshProUGUI component.");
            return;
        }

        text.text = "Day " + day;
    }

    private void DestroyAllNPCs()
    {
        if (NPCParent != null)
        {
            foreach (Transform child in NPCParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("NPCParent is null cannot destroy NPCs");
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
