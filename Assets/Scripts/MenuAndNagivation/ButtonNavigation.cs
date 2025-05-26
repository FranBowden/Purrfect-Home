using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonNavigation : MonoBehaviour
{
     public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }
    public void MainGame()
    {
        SceneManager.LoadScene(2);

    }

    public void exitGame()
    {
        Application.Quit();
    }
}
