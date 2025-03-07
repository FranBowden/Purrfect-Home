using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    private InputSystem_Actions.PlayerActions player;
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        player = inputSystem.Player;


        //example
        //pancamera = getcompotent<cameraPanning>();
        //player.pancamera.performed += ctx => function();
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
