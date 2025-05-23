using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] TimeManager time;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource walkingAudio;
    private Vector3 lastInput;
    private bool sprint = false;
    public float speed = 1f;
    public float sprintSpeed = 1.7f;
    private float currentSpeed;

    void Start()
    {
        walkingAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = speed;
    }

    private void Update()
    {

        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }
   //    animator.SetBool("isWalking", rb.linearVelocity.magnitude > 0f);
    }
    public void move(Vector2 input)
    {
        if (!sprint)
        {
            rb.linearVelocity = input * currentSpeed;
        }
       
       
        if (input != Vector2.zero)
        {
            if (!walkingAudio.isPlaying) //stops the audio from restarting constantly
            {
                walkingAudio.Play();
            }
            animator.SetBool("isWalking", true);
            animator.SetFloat("CurrentInputX", input.x);
            animator.SetFloat("CurrentInputY", input.y);
            lastInput = input; //storing the last input before it gets reset to 0
        }
        else
        {
            
            walkingAudio.Stop();
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", lastInput.x);
            animator.SetFloat("LastInputY", lastInput.y);

         
        }


    }

    public void StartSprinting()
    {
        currentSpeed = sprintSpeed;
    }

    public void StopSprinting()
    {
        currentSpeed = speed;
    }

}
