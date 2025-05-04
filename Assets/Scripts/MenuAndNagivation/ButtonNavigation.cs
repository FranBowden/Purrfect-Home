using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonNavigation : MonoBehaviour
{
     public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void settings()
    {

    }

    public void exitGame()
    {
        Application.Quit();
    }
}
