using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 5f;
    private Animator animator;
    private bool canMove = true;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = controller.isGrounded;
        Debug.Log("IsGrounded :" + isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
       
        animator.SetBool("Jump", false);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        Vector3 moveDestination = transform.position + move;
        if (moveDestination.x > 0 || moveDestination.z >0)
        {
           
            animator.SetBool("Walk", true);
        }


        if (isGrounded == true)
        {
            animator.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            canMove = false;

            animator.SetBool("Jump", true);
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
         
        }
        velocity.y += gravity * Time.deltaTime;

        GetComponent<NavMeshAgent>().destination = moveDestination;
        controller.Move(velocity * Time.deltaTime);

        StartCoroutine(CanMove());
    }
   
    IEnumerator CanMove()
    {
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(2f);
        canMove = true;
}
}
