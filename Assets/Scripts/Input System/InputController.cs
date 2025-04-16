using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputController : MonoBehaviour
{
    [SerializeField] InteractionDetection ID;
    //[SerializeField] Dialogue Dialogue;
    [SerializeField] Journal Journal;
    private InputSystem_Actions inputSystem;
    private InputSystem_Actions.PlayerActions player;
    private PlayerMovement movement;
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        player = inputSystem.Player;
        movement = GetComponent<PlayerMovement>();


      
        player.Interact.performed += ctx => ID.interactableInRange?.Interact();
        player.Journal.performed += ctx => Journal.ToggleJournal();
        player.Sprint.started += ctx => movement.StartSprinting(); // When Shift is pressed
        player.Sprint.canceled += ctx => movement.StopSprinting();
        // player.Interact.performed += ctx => Dialogue.CloseDiologue();

    }
    
    private void FixedUpdate()
    {  
        movement.move(player.Move.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        player.Enable();
      
    }


    private void OnDisable()
    {
        player.Disable();
    }
}
