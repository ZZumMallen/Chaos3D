using System.Collections;
using UnityEngine;

namespace Chaos
{
    public class BlueTarget : MonoBehaviour
    {
        Rigidbody rb;
        
        [SerializeField] bool moveLeftInstead;
        //[SerializeField] private float m_enemySpeed = 0f;
        [SerializeField] float m_enemyHealth = 200f;

        private Color originalColor;
        private Renderer enemyRenderer;
        public float flashDuration = 0.1f;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            enemyRenderer = GetComponent<Renderer>();
            originalColor = enemyRenderer.sharedMaterial.color;
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
            enemyRenderer.sharedMaterial.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            enemyRenderer.sharedMaterial.color = originalColor;
        }

    }
}

