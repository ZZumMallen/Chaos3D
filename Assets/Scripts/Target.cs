using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Chaos
{
    public class Target : MonoBehaviour
    {

        Rigidbody rb;

        [SerializeField] bool moveLeftInstead;    
        [SerializeField] float m_enemySpeed;
        [SerializeField] float m_enemyHealth = 100f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (m_enemyHealth == 0f) Debug.LogError($"No {this.name} health you dummy!");
            if (m_enemySpeed == 0f) Debug.LogError($"No {this.name} speed you dummy!");
        }

        private void Update()
        {
            if (m_enemyHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (moveLeftInstead)
            {
                rb.linearVelocity = new Vector3(-1f * m_enemySpeed, rb.linearVelocity.y, 0f);
            }
            else
            {
                rb.linearVelocity = new Vector3(1f * m_enemySpeed, rb.linearVelocity.y, 0f);
            }
        }

        public void TakeDamage(float damage)
        {
            m_enemyHealth -= damage;
        }     
    }
}

