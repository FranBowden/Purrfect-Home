using UnityEngine;
using UnityEngine.UI;

public class CleaningMechanics : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject Bag;
    [SerializeField] GameObject litter;
    [SerializeField] GameObject poop;
    [SerializeField] GameObject tray;
    [SerializeField] GameObject cleaningBar;
    [SerializeField] GameObject cleaningParentMenu;


    public Vector2 poopPosition = new Vector2(-0, -9);
    public float jitterAmount = 35f;

    private CatOptionsMenuController catOptions;

    private bool completeOnce = false;
    private void Start()
    {
        catOptions = GetComponent<CatOptionsMenuController>();
    }
    private void Update()
    {
        if (catOptions.cleaningMenuShowing && !completeOnce)
        {

            GeneratePoop();
            completeOnce = true;
        }
    }
    void GeneratePoop()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject Poop = Instantiate(poop);
            Poop.transform.SetParent(tray.transform);
            Poop.transform.localPosition = GeneratePositions();

            Poop.GetComponent<Button>().onClick.AddListener(() => RemovePoop(Poop));
        }
    }


    public Vector2 GeneratePositions()
    {
        float offsetX = Random.Range(-jitterAmount, jitterAmount);
        return poopPosition + new Vector2(offsetX, 0);
    }

    public void RemovePoop(GameObject poop)
    {
        Destroy(poop);
        Reward();
    }

    void Reward()
    {
        DisplayCatInformation catInfo = PlayerController.Instance.catViewing.GetComponent<DisplayCatInformation>();
        CatOptionsMenuController catOptions = gameObject.GetComponent<CatOptionsMenuController>();

        AdoptionShelterReputation.Instance.SetCurrentPoints(5);
        if (catInfo != null && catInfo.catData.hygiene < 1)
        {
            catInfo.catData.hygiene += 0.1f; //increase by one
            catOptions.ToggleMenu(catOptions.cleanMenu);
        }
    }
}
