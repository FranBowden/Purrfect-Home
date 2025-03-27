using UnityEngine;

public class Journal : MonoBehaviour
{
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
