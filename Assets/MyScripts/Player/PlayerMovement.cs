using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : RPGMonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected bool grounded;

    [SerializeField] protected float groundDrag = 5f;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected float jumpCoolDown = 0.24f;
    [SerializeField] protected bool readyTojump;
  //  [SerializeField] protected RaycastHit raycastHit; Sử dụng cho outline
    private Rigidbody rb;
    [SerializeField] private float speed = 5.0f;
    private float rotationSpeed = 50.0f;
    private float playerHeight = 2f;
    public static bool isRunning;

    private PlayerAnim playerAnim;
    protected override void LoadComponents()
    {
        this.LoadRigidbody();
        this.LoadPlayerAnimation();
    }
    protected void LoadRigidbody()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody>();
        //Debug.LogWarning(transform.name + "|LoadRigidbody|", gameObject);
    }
    protected void LoadPlayerAnimation()
    {
        if (this.playerAnim != null) return;
        this.playerAnim = transform.parent.GetComponent<PlayerAnim>();
       // Debug.LogWarning(transform.name + "|LoadPlayerAnimation|", gameObject);
    }

    void Start()
    {
        playerAnim.IdlingAnimation(true);
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
        playerAnim.FallAnimation(grounded);

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
            playerAnim.IdlingAnimation(false);
            isRunning = true;
        }

        else
        {
            playerAnim.IdlingAnimation(true);
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


