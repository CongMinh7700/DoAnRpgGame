using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Input
    [SerializeField] protected float horizontalInput;
    [SerializeField] protected float verticalInput;
    //Their code
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;

    //
    public float airMutiple;
    public bool readyTojump;
    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;


    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
    

    private NavMeshAgent agent;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        rb.freezeRotation = true;
        readyTojump = true;


    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }
    private void FixedUpdate()
    {
        MovePlayer();
        if (grounded && readyTojump)
        {
            agent.enabled = true;
            //  moveSpeed = 100f;
        }
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log(grounded);

        if (Input.GetKey(jumpKey) && readyTojump && grounded)
        {
            readyTojump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);

        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * airMutiple, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);

        }
      
    }

    private void Jump()
    {
        agent.enabled = false;
        moveSpeed = 10f;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }
    private void ResetJump()
    {
        readyTojump = true;



    }
}
