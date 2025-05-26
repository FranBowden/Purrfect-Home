using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    //needed for input system
    private void Start()
    {
        gameObject.SetActive(false);

    }
    public void pauseMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            PauseController.SetPause(false);
        }
        else
        {
            gameObject.SetActive(true);
            PauseController.SetPause(true);
        }
    }
}
