using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed;
    public float playerRunSpeed;
    public float groundDrag;


    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool jumpReady;
    public bool DoobleJumpReady;


    [Header("keyCodes")]
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode Dooblejumpkey = KeyCode.F;
    public KeyCode RunKey = KeyCode.LeftShift;

    [Header("Goround check")]
    public float playerHeight;
    public LayerMask Ground;
    bool groundedPlayer;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update() {
        //ground check
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        
        MyInput();
        SpeedControl();

        //hadle drag
        if(groundedPlayer)
        rb.drag = groundDrag;
        else
        rb.drag = 0;
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(RunKey) )
        {
            playerSpeed = playerRunSpeed;
            // airMultiplier = 1;
        }else
        {
            playerSpeed = 4;
            // airMultiplier = 0.5f;
        }

        //when to jump 
        if(Input.GetKey(jumpkey) && jumpReady && groundedPlayer)
        {
            jumpReady = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if(Input.GetKey(Dooblejumpkey) && DoobleJumpReady && groundedPlayer == false)
        {
            DoobleJump();
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on grounded
        if(groundedPlayer)
            rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);

        //in air
        else if(!groundedPlayer)
            rb.AddForce(moveDirection.normalized * playerSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        } 
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        DoobleJumpReady = true;
    }
    private void ResetJump()
    {
        jumpReady = true;
    }


    private void DoobleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        DoobleJumpReady = false;
    }
}
