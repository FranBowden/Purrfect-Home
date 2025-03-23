using UnityEngine;

public class Cat : MonoBehaviour, IInteractable
{
    public bool catMenuOn { get; private set; }
    public string catID { get; private set; }
    public GameObject catMenu;

    void Start()
    {
        catID ??= Global.GenerateUniqueID(gameObject);
    }
    public bool CanInteract()
    {
        return !catMenuOn;

    }

    public void Interact()
    {
        if (!CanInteract()) return;
        displayDesciptionCat();

    }

    private void displayDesciptionCat()
    {

        catMenu.SetActive(true);
        Debug.Log("Showing cat description");

    }

}
