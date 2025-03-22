using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    
    public bool isOn { get; private set; }
    public string computerID { get;private set; }
    public GameObject computerScreen;

    void Start()
    {
        computerID ??= Global.GenerateUniqueID(gameObject);
    }
    public bool CanInteract()
    {
        return !isOn;

    }

    public void Interact()
    {
        if (!CanInteract()) return;
        turnOnComputer();

    }

    private void turnOnComputer()
    {
      
         computerScreen.SetActive(true);
        Debug.Log("Turn on Computer");
        
    }
   

}
