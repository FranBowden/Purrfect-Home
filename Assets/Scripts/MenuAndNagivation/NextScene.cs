using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    public void loadScene()
    {
        SceneManager.LoadScene(2);
    }
}