using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float speed = 10.0f;
    float rotationSpeed = 50.0f;
    Animator animator;
    public LayerMask whatIsGround;
    public bool grounded;
    public float playerHeight = 2f;
    public float groundDrag = 5f;
    public float jumpForce = 5f;
    public float jumpCoolDown = 0.24f;
    public bool readyTojump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idling", true);
        readyTojump = true;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
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
        float tranlation = Input.GetAxis("Vertical") * speed;
        float rotaion = Input.GetAxis("Horizontal") * rotationSpeed;
        tranlation *= Time.deltaTime;
        rotaion *= Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f, rotaion, 0f);
        rb.MovePosition(rb.position + transform.forward * tranlation);
        rb.MoveRotation(rb.rotation * turn);

        if (tranlation != 0)
            animator.SetBool("Idling", false);
        else
            animator.SetBool("Idling", true);
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