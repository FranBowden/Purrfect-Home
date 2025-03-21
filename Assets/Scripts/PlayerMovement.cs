using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator animator;
    public float speed = 1f;
    private Vector3 lastInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void move(Vector2 input)
    {
        rb.linearVelocity = input * speed;
      

        
        
        if (input != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("CurrentInputX", input.x);
            animator.SetFloat("CurrentInputY", input.y);
            lastInput = input; //storing the last input before it gets reset to 0
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", lastInput.x);
            animator.SetFloat("LastInputY", lastInput.y);
        }
        

    }

}
