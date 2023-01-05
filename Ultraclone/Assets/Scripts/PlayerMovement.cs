using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float maxDefaultSpeed = 10;
    [SerializeField]
    float
        maxSprintSpeed = 15,
        maxAirSpeed = 15,
        force = 10,
        groundDrag = 5,
        airMultiplier = .4f,
        jumpForce = 5;
    bool
        canJump = true;
    Vector3
        moveDirection;
    Rigidbody
        rb;

    float
        maxSpeed;

    [Header("Ground Check")]
    [SerializeField]
    Transform
        groundCheck;
    [SerializeField]
    float
        groundCheckRadius = .1f,
        jumpCooldown = .2f;
    [SerializeField]
    LayerMask
        groundLayer;
    bool
        isGrounded;


    //Inputs
    float
        hInput,
        vInput;


    PlayerWeaponController 
        weaponController;

    
    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = maxDefaultSpeed;
        rb = GetComponent<Rigidbody>();
        weaponController = GetComponent<PlayerWeaponController>();
    }

    private void FixedUpdate()
    {
        MoveDirection();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        PlayerInput();
        
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }


    void PlayerInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        weaponController.SetCanShoot(true);
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            maxSpeed = maxSprintSpeed;
            weaponController.SetCanShoot(false);
        }
        else if(!isGrounded)
        {
            maxSpeed = maxAirSpeed;
        }
        else
        {
            maxSpeed = maxDefaultSpeed;
        }

        if(Input.GetKey(KeyCode.Space) && canJump && isGrounded)
        {
            Jump();
        }
    }

    void MoveDirection()
    {
        moveDirection = transform.forward * vInput + transform.right * hInput;
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * force, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * force * airMultiplier, ForceMode.Force);
        }
    }

  

    void SpeedControl()
    {
        Vector3 vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        vel = Vector3.ClampMagnitude(vel, maxSpeed);
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        canJump = false;
        Invoke("ResetJump", jumpCooldown);

    }

    void ResetJump()
    {
        canJump = true;
    }
}
