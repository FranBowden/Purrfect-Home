#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class MenuToggles : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject settingsMenu;
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pausedMenu.SetActive(false);
    }

    public void ReturnToPauseMenu()
    {
        settingsMenu.SetActive(false);
        pausedMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        settingsMenu.SetActive(false);
        pausedMenu.SetActive(false);

        PauseController.SetPause(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false; 
        #else
            Application.Quit();
        #endif

    }
}
