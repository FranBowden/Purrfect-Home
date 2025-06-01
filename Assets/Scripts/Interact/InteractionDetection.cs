using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetection : MonoBehaviour
{
    public IInteractable interactableInRange = null;
    public GameObject interactionIcon;
    public GameObject catIcon;

    private HashSet<Collider2D> catsInRange = new HashSet<Collider2D>();
    private HashSet<Collider2D> otherCollisions = new HashSet<Collider2D>();


    //interaction radius is defined under player -> interactable detector -> and its the radius of circle collider 2D
    void Start()
    {
        interactionIcon.SetActive(false);
        catIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            if (collision.CompareTag("Cat"))
            {
                catsInRange.Add(collision);
                catIcon.SetActive(true);
            }
            else
            {
                otherCollisions.Add(collision);
                interactionIcon.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            catsInRange.Remove(collision);
            if (catsInRange.Count == 0)
            {
                catIcon.SetActive(false);
            }
        }
        else 
        {
            otherCollisions.Remove(collision);
            if (otherCollisions.Count == 0)
            {
                interactionIcon.SetActive(false);
            }
        }

        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
        }
    }


}
