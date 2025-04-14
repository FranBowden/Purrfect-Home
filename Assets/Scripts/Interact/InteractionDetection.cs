using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetection : MonoBehaviour
{
    public IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //interaction radius is defined under player -> interactable detector -> and its the radius of circle collider 2D
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
