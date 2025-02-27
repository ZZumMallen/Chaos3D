using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider ccol;

    [Header("Lateral Movement Settings")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float accelerationTime = 0.1f;
    private float horizontalInput;
    private float currentSpeed = 0f;

    [Header("Jump Settings")]
    private float jumpHeight = 5f;
    [SerializeField] private float fallMultiplier = 3f;
    [SerializeField] private float lowJumpMultiplier = 2.5f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private bool jumpKeyPressed;
    private bool jumpKeyReleasedEarly;

    [Header("Jump Assistance")]
    [SerializeField] private float groundCheckBuffer = 0.2f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ccol = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpKeyPressed = true;
            jumpKeyReleasedEarly = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpKeyReleasedEarly = true;
        }
    }

    private void FixedUpdate()
    {
        HandleGroundCheck();
        HandleJump();
        ApplyMovement();
        ApplyBetterJumpPhysics();
    }

    private void HandleGroundCheck()
    {
        isGrounded = CheckIfGrounded();

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }

    private void HandleJump()
    {
        if (jumpKeyPressed && coyoteTimeCounter > 0f)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
            coyoteTimeCounter = 0f;
        }
        jumpKeyPressed = false;
    }

    private void ApplyMovement()
    {
        float targetSpeed = horizontalInput * maxSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.fixedDeltaTime / accelerationTime);
        rb.linearVelocity = new Vector3(currentSpeed, rb.linearVelocity.y, 0);
    }

    private void ApplyBetterJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && jumpKeyReleasedEarly)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, rb.linearVelocity.z);
            jumpKeyReleasedEarly = false;
        }
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckBuffer, groundLayer);
    }
}

