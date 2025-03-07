using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void move(Vector2 input)
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection.x = input.x;
        movementDirection.y = input.y;
        controller.Move(transform.TransformDirection(movementDirection) * speed * Time.deltaTime);
    }
    
}
