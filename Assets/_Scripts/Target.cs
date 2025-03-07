using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Chaos
{
    public class Target : MonoBehaviour
    {

        Rigidbody rb;

        [Tooltip("Default: Move Right")]
        [SerializeField] bool moveLeftInstead;    
        [SerializeField] float m_enemySpeed;
        [SerializeField] float m_enemyHealth = 100f;

        private Color originalColor;
        private Renderer enemyRenderer;

        public float flashDuration = 0.1f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (m_enemyHealth == 0f) Debug.LogError($"No {this.name} health you dummy!");
            if (m_enemySpeed == 0f) Debug.LogError($"No {this.name} speed you dummy!");
        }

        private void Start()
        {
            enemyRenderer = GetComponent<Renderer>();
            originalColor = enemyRenderer.material.color;
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

        public void ApplyDamage(float damage)
        {
            m_enemyHealth -= damage;

            if (m_enemyHealth <= 0f)
            {
                Destroy(gameObject);
            }

            StartCoroutine(FlashEffect());
        }

        private IEnumerator FlashEffect()
        {      
            enemyRenderer.material.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            enemyRenderer.material.color = originalColor;
        }     
    }
}

