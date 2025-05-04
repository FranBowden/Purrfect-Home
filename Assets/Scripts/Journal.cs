using UnityEngine;

public class Journal : MonoBehaviour
{
    public static Journal Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void ToggleJournal()
   {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
   }
}
