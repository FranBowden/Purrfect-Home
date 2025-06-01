using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject SettingsMenu;

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
            SettingsMenu.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            SettingsMenu.SetActive(false);
            PauseController.SetPause(true);
        }
    }
}
