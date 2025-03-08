using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Chaos.Target
{
    [RequireComponent(typeof(TargetHealth))]
    [RequireComponent(typeof(TargetMovement))]

    public class Target : MonoBehaviour
    {
        [SerializeField] Stats stats;
        private TargetHealth _health;
        private TargetMovement _movement;

        private void Awake()
        {
            _health = GetComponent<TargetHealth>();
            _movement = GetComponent<TargetMovement>();
            
        }

        private void Start()
        {
            _health.Stats = stats;
        }

        private void FixedUpdate()
        {
            // Apply physics in FixedUpdate
            _movement.HandleData(stats.moveSpeed);
            _movement.Move();          
        }

        public void ApplyDamage(float damage)
        {
            _health.ApplyDamage(damage);
            //_health.Stats = stats;
        }

    }
}

