using UnityEngine;

namespace Chaos
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private float maxHeight = 10f;
        [SerializeField] private float minHeight = 6f;
        [SerializeField] private float travelSpeed = 10f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }
        
    }
}

