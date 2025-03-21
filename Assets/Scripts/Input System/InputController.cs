using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    private InputSystem_Actions.PlayerActions player;
    private PlayerMovement movement;
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        player = inputSystem.Player;
        movement = GetComponent<PlayerMovement>();


        //example
        //pancamera = getcompotent<cameraPanning>();
        //player.pancamera.performed += ctx => function();

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
