using System.Xml.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public bool hasCompanionNPC;
    public GameObject companionNPC;
    public GameObject catSelected;
    public GameObject catViewing;

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
