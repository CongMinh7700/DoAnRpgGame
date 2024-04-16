using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected bool grounded;

    [SerializeField] protected float groundDrag = 5f;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected float jumpCoolDown = 0.24f;
    [SerializeField] protected bool readyTojump;

    private Rigidbody rb;
    private float speed = 10.0f;
    private float rotationSpeed = 50.0f;
    private Animator animator;
    private float playerHeight = 2f;
    public static bool isRunning;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idling", true);
        readyTojump = true;
        rb.freezeRotation = true;
    }


    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f - 0.7f, whatIsGround);
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        Movement();
        if (Input.GetKey(KeyCode.Space) && readyTojump && grounded)
        {
            readyTojump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);

        }
        
    }
    public void Movement()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
        rb.MovePosition(rb.position + transform.forward * translation);
        rb.MoveRotation(rb.rotation * turn);

        if (translation != 0)
        {
            animator.SetBool("Idling", false);
            isRunning = true;
        }

        else
        {
            animator.SetBool("Idling", true);
            isRunning = false;
        }
     
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyTojump = true;
    }

    


}


