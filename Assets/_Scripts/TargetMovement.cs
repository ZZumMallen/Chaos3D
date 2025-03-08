using UnityEngine;

namespace Chaos.Target
{
    public class TargetMovement : MonoBehaviour
    {

        

        private Rigidbody _rb;

        private float _moveSpeed;
        private bool _moves;

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            
                      
        }

        public void HandleData(float speed)
        {
            _moveSpeed = speed;

            if (speed == 0f)
            {
                _moves = false;
            }
            else
            {
                _moves = true;
            }
        }

        public void Move()
        {
            if (_moves)
            {
                _rb.linearVelocity = new Vector3(_moveSpeed, _rb.linearVelocity.y, 0f);
            }
          
        }
    }
}

