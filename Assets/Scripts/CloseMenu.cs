using TMPro;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{

  

    public void closeMenu()
    {

        gameObject.SetActive(false);
     
        Destroy(gameObject);

    }

    
}
