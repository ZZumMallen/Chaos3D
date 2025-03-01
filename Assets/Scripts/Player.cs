using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace Chaos
{
    public class Player : MonoBehaviour
    {
        public ConstantForce gravity;
        private readonly float gravConst = -9.81f;
        private Rigidbody rb;

        #region Declarations
        [Header("Lateral Movement Settings")]
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float accelerationTime = 0.1f;
        private float horizontalInput;
        private bool facingRight = true;
        private float currentSpeed = 0f;

        [Header("Jump Settings")]
        [SerializeField] private float jumpVelocity = 7f;
        [SerializeField] private float fallSpeedMultiplier= 2f;
        [SerializeField] private float shortFallSpeedMultiplier = 4f;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private LayerMask groundLayer;
        
        private bool isGrounded;
        private bool jumpKeyPressed;
        private bool jumpKeyReleasedEarly;

        [Header("Jump Assistance")]
        [SerializeField] private float groundCheckBuffer = 0.2f;
        [SerializeField] private float coyoteTime = 0.2f;
        private float coyoteTimeCounter;
        #endregion

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            gravity = gameObject.AddComponent<ConstantForce>();
            gravity.force = new Vector3(0f, gravConst, 0f);
            rb.useGravity = false;
        }

        void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpKeyPressed = true;
                jumpKeyReleasedEarly = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpKeyReleasedEarly = true;
            }
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            FlipPlayer();            
            HandleJump();            
            ApplyBetterJumpPhysics();
            HandleGroundCheck();
        }

        private void ApplyMovement()
        {
            float targetSpeed = horizontalInput * maxSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.fixedDeltaTime / accelerationTime);
            rb.linearVelocity = new Vector3(currentSpeed, rb.linearVelocity.y, 0);
        }

        private void FlipPlayer()
        {
            if (facingRight && horizontalInput < 0)
            {
                gameObject.transform.Rotate(0f, -180f, 0f, Space.Self);
                facingRight = false;
            }

            if (!facingRight && horizontalInput > 0)
            {
                gameObject.transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }

        #region JumpFunctions

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
                rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
                coyoteTimeCounter = 0f;
            }

            jumpKeyPressed = false;
        }

        private void ApplyBetterJumpPhysics()
        {
            if (rb.linearVelocity.y > 0 && jumpKeyReleasedEarly)
            {
                gravity.force = new Vector3(0f, gravConst * shortFallSpeedMultiplier, 0f);

                if (rb.linearVelocity.y <= 0.1f)
                {
                    jumpKeyReleasedEarly = false;
                }

                //Debug.Log($"Short Fallspeed: {gravity.force}");
            }

            else if (rb.linearVelocity.y < 0 && !isGrounded)
            {
                gravity.force = new Vector3(0f, gravConst * fallSpeedMultiplier, 0f);
                //Debug.Log($"Normal Fallspeed: {gravity.force}");                
            }

            else
            {
                gravity.force = new Vector3(0f, gravConst, 0f);
            }
        }

        private bool CheckIfGrounded()
        {
            // having only one raycast check really affected the coyote time so I added these to 
            Vector3 positionRight = groundCheckPoint.position + new Vector3(0.25f,0f,0f);
            Vector3 positionLeft = groundCheckPoint.position + new Vector3(-0.25f,0f,0f);

            //Debug.DrawRay(positionLeft, Vector3.down, Color.red);
            //Debug.DrawRay(positionRight, Vector3.down, Color.red);

            return
            Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckBuffer, groundLayer) ||
            Physics.Raycast(positionRight, Vector3.down, groundCheckBuffer, groundLayer) ||
            Physics.Raycast(positionLeft, Vector3.down, groundCheckBuffer, groundLayer);
        }

        #endregion
    }
}