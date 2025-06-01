using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdoptionShelterReputation : MonoBehaviour
{
    public static AdoptionShelterReputation Instance { get; private set; }

   
    [SerializeField] private Sprite[] stars;
    [SerializeField] private GameObject ratingStars;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject PointsPrefab;

    public Color color1;
    public Color color2;


    public float currentPoints = 0;
    private readonly float MaxPoints = 500;
    private Image ratingStarImage;

    public int starRating = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ratingStarImage = ratingStars.GetComponent<Image>();
        RecalculateStars();
    }

    public void SetCurrentPoints(int newPoints)
    {
        currentPoints += newPoints;
        PlayerController.Instance.Money = currentPoints;
        ShowPoints("+ " + newPoints, color2);

        RecalculateStars();
    }

    public void RemovePoints(int pointsRemoved)
    {
      currentPoints -= pointsRemoved;
        PlayerController.Instance.Money = currentPoints;

        ShowPoints("- " + pointsRemoved, color1);
        RecalculateStars();
    }

    private void ShowPoints(string pointsMsg, Color color)
    {
       GameObject pointsUI = Instantiate(PointsPrefab);
        pointsUI.transform.SetParent(canvas.transform, false);
        TextMeshProUGUI pointsTMP = pointsUI.GetComponent<TextMeshProUGUI>();
       pointsTMP.color = color;
       pointsTMP.text = pointsMsg;
    }

    void RecalculateStars()
    {
        float decimalResult = Mathf.Clamp01((float)currentPoints / MaxPoints);
        starRating = Mathf.RoundToInt(decimalResult * 5);
        DisplayStars(starRating, ratingStarImage);
    }

    public void DisplayStars(int starRating, Image ratingStarImage)
    {
        ratingStarImage.sprite = stars[Mathf.Clamp(starRating, 0, stars.Length - 1)];
    }
}
