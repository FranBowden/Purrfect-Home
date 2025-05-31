using System.Xml.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public bool hasCompanionNPC;
    public GameObject companionNPC;
    public GameObject catSelected;
    public GameObject catViewing;

    [Header("Shop Items: ")]
    public int CatFood = 0;
    public int CatLitter = 0;
    public int CatPod = 0;
    [Header("Currency: ")]
    public float Money = 1000;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
}
