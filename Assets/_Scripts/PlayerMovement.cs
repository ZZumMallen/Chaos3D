using UnityEngine;

namespace Chaos
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Lateral Movement Settings")]
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float accelerationTime = 0.1f;
        
        private float _horizontalInput;
        private bool _facingRight = true;
        private float _currentSpeed = 0f;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void ProcessInput()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
        }

        public void Move()
        {
            float targetSpeed = _horizontalInput * maxSpeed;
            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.fixedDeltaTime / accelerationTime);
            _rb.linearVelocity = new Vector3(_currentSpeed, _rb.linearVelocity.y, 0);
        }

        public void FlipPlayer()
        {
            if (_facingRight && _horizontalInput < 0)
            {
                transform.Rotate(0f, -180f, 0f, Space.Self);
                _facingRight = false;
            }
            else if (!_facingRight && _horizontalInput > 0)
            {
                transform.Rotate(0f, 180f, 0f);
                _facingRight = true;
            }
        }
    }
}
