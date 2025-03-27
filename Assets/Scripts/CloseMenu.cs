using TMPro;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{

  
    public void closeCatMenu()
    {
        Destroy(gameObject);
    }
    public void closeMenu()
    {
        gameObject.SetActive(false);
    }
    
}
