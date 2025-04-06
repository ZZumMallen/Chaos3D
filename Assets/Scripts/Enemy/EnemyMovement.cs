using Chaos.Player;
using UnityEngine;

namespace Chaos.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        private Transform frontCheck;

        private float _moveSpeed = 2;
        private bool _movingRight;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            frontCheck = transform.GetChild(0);
            _movingRight = true;
        }

        private void Update()
        {
            CheckCollider();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void CheckCollider()
        {
            if (Physics.Raycast(frontCheck.position, frontCheck.TransformDirection(Vector3.forward), out RaycastHit hit, 0.3f))            
            {
                HandleHit(hit);
            }
        }

        private void HandleHit(RaycastHit hit)
        {
            if (hit.collider != TryGetComponent(out PlayerController playercontroller))
            {
                Debug.Log(Time.time);
                Flip();
            }

            Move();
        }

        public void HandleData(float _)
        {
            // Don't  Delete Idiot
        }

        public void Move()
        {
            if (_movingRight)
            {
                _rb.linearVelocity = new Vector3(1 * _moveSpeed, _rb.linearVelocity.y, 0);
            }

            if (!_movingRight)
            {
                _rb.linearVelocity = new Vector3(-1 * _moveSpeed, _rb.linearVelocity.y, 0);
            }
        }

        private void Flip()
        {
            if (_movingRight)
            {
                transform.Rotate(0f, -180f, 0f, Space.Self);

                _movingRight = false;

                Move();
            }
            else if (!_movingRight)
            {
                transform.Rotate(0f, 180f, 0f, Space.Self);

                _movingRight = true;

                Move();
            }
        }
    }
}
