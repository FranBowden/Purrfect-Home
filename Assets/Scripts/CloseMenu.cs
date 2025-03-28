using TMPro;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{

    public void destroyMenu()
    {
        Destroy(gameObject);
    }
    public void closeMenu()
    {
        gameObject.SetActive(false);
    }
    
}
