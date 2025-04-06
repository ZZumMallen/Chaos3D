using System;
using System.Collections;
using Chaos.Player;
using UnityEngine;

namespace Chaos.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyMovement))]

    public class Enemy : MonoBehaviour
    {
        [SerializeField] Stats stats;
        private EnemyHealth _health;
        private EnemyMovement _movement;
        private int _pointValue;
        private float _myDamage;
        

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();
            _movement = GetComponent<EnemyMovement>();
            _myDamage = stats.meleeDamage;
        }

        private void Start()
        {
            if (stats != null)
            {
                _health.SetStats(stats);
                _pointValue = stats.pointValue;
            }
            else
            {
                _pointValue = 10;
            }
        }

        private void FixedUpdate()
        {
            if (stats != null)
            {
                _movement.HandleData(stats.moveSpeed);
                _movement.Move();
            }
        }

        public void ApplyDamage(float damage)
        {
            _health.HandleDamage(damage);
            //_health.Stats = stats;
        }

        public int GetPointValue()
        {
            return _pointValue;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.HandleDamage(_myDamage);
            }
        }
    }
}

