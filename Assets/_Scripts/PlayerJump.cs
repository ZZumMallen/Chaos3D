using UnityEngine;

namespace Chaos.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private float jumpVelocity = 7f;
        [SerializeField] private float fallSpeedMultiplier = 2f;
        [SerializeField] private float shortFallSpeedMultiplier = 4f;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckBuffer = 0.2f;
        [SerializeField] private float coyoteTime = 0.2f;

        private Rigidbody _rb;
        private ConstantForce _gravity;
        private readonly float _gravityConstant = -9.81f;
        
        private bool _isGrounded;
        private bool _jumpKeyPressed;
        private bool _jumpKeyReleasedEarly;
        private float _coyoteTimeCounter;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _gravity = gameObject.AddComponent<ConstantForce>();
            _gravity.force = new Vector3(0f, _gravityConstant, 0f);
            _rb.useGravity = false;
        }

        public void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpKeyPressed = true;
                _jumpKeyReleasedEarly = false;
                if (!_isGrounded) _coyoteTimeCounter = 0f;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _jumpKeyReleasedEarly = true;
            }
        }

        public void HandleJump()
        {
            if (_jumpKeyPressed && _coyoteTimeCounter > 0f)
            {                
                _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
                _coyoteTimeCounter = 0f;
            }

            _jumpKeyPressed = false;            
            ApplyBetterJumpPhysics();
        }

        public void HandleGroundCheck()
        {
            _isGrounded = CheckIfGrounded();

            if (_isGrounded)
            {
                _coyoteTimeCounter = coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.fixedDeltaTime;
            }
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }

        private void ApplyBetterJumpPhysics()
        {
            if (_rb.linearVelocity.y > 0 && _jumpKeyReleasedEarly)
            {
                _gravity.force = new Vector3(0f, _gravityConstant * shortFallSpeedMultiplier, 0f);

                if (_rb.linearVelocity.y <= 0.1f)
                {
                    _jumpKeyReleasedEarly = false;
                }
            }
            else if (_rb.linearVelocity.y < 0 && !_isGrounded)
            {
                _gravity.force = new Vector3(0f, _gravityConstant * fallSpeedMultiplier, 0f);
            }
            else
            {
                _gravity.force = new Vector3(0f, _gravityConstant, 0f);
            }
        }

        private bool CheckIfGrounded()
        {
            return Physics.SphereCast(groundCheckPoint.position + Vector3.up * 0.1f, 0.2f, Vector3.down,out _, groundCheckBuffer + 0.1f,groundLayer);
        }
    }
}
