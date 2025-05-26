using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputController : MonoBehaviour
{
    [SerializeField] InteractionDetection ID;
    private InputSystem_Actions inputSystem;
    private InputSystem_Actions.PlayerActions player;
    private PlayerMovement movement;
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        player = inputSystem.Player;
        movement = GetComponent<PlayerMovement>();
      
        player.Interact.performed += ctx => ID.interactableInRange?.Interact();
        player.Sprint.started += ctx => movement.StartSprinting();
        player.Sprint.canceled += ctx => movement.StopSprinting();

        player.Escape.performed += ctx => PauseMenu.Instance.pauseMenu();
    }

    private void OnEnable()
    {
        player.Enable();

    }

    private void OnDisable()
    {
        player.Disable();
    }

   
    private void FixedUpdate()
    {  
        movement.move(player.Move.ReadValue<Vector2>());
    }
  
}
