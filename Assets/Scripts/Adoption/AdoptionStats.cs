using UnityEngine;

public class AdoptionStats : MonoBehaviour
{
    public static AdoptionStats Instance { get; private set; }
    public int numCatsAdopted = 0;
    public GameObject adoptionMessagePrefab;

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
