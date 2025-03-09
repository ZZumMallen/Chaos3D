using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

namespace Chaos
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private float maxHeight = 25f;
        [SerializeField] private float minHeight = 6f;
        [SerializeField] private float maxSpeed = 2f;

        private float _currentSpeed = 0f;
        private bool _atMaxHeight;
        private bool _atMinHeight;
        private bool _isMoving = true;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            _currentSpeed = maxSpeed;
            _rb.linearVelocity = new Vector3(0f, _currentSpeed, 0f);
        }

        void FixedUpdate()
        {
            if (_isMoving)
            {
                CheckPosition();
            }
        }

        private void CheckPosition()
        {
            float currentY = transform.position.y;

            
            if (currentY >= maxHeight && _currentSpeed > 0)
            {
                _atMaxHeight = true;
                _atMinHeight = false;
                _isMoving = false;
                _rb.linearVelocity = Vector3.zero; 
                Flip();
            }
            
            else if (currentY <= minHeight && _currentSpeed < 0)
            {
                _atMinHeight = true;
                _atMaxHeight = false;
                _isMoving = false;
                _rb.linearVelocity = Vector3.zero; 
                Flip();
            }
        }

        private void Flip()
        {
            StopAllCoroutines();
            if (_atMaxHeight)
            {               
                StartCoroutine(Descend());
            }

            if (_atMinHeight)
            {               
                StartCoroutine(Ascend());
            }
        }

        IEnumerator Descend()
        {
            Debug.Log("Starting descent after pause");
            yield return new WaitForSeconds(2f);

            // Change to negative speed to go down
            _currentSpeed = -maxSpeed;
            _rb.linearVelocity = new Vector3(0f, _currentSpeed, 0f);
            _isMoving = true;
        }

        IEnumerator Ascend()
        {
            Debug.Log("Starting ascent after pause");
            yield return new WaitForSeconds(2f);

            // Change to positive speed to go up
            _currentSpeed = maxSpeed;
            _rb.linearVelocity = new Vector3(0f, _currentSpeed, 0f);
            _isMoving = true;
        }       
    }
}
