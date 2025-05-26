using System;
using UnityEngine;
using UnityEngine.UI;

public class AdoptionShelterReputation : MonoBehaviour
{
    public static AdoptionShelterReputation Instance { get; private set; }

    [SerializeField] private Sprite[] stars;
    [SerializeField] private GameObject ratingStars;
    [SerializeField] private float currentPoints = 0;
    private readonly float MaxPoints = 1000;
    private Image ratingStarImage;
   
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
        RecalculateStars();
    }

    void RecalculateStars()
    {

        float decimalResult = currentPoints / MaxPoints;
        int starRating = Mathf.FloorToInt(decimalResult * 5);
        DisplayStars(starRating);
    }

    void DisplayStars(int starRating)
    {
        ratingStarImage.sprite = stars[starRating];
    }
}
