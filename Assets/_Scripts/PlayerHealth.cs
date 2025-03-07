using System.Collections;
using UnityEngine;

namespace Chaos
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 1000f;
        [SerializeField] private float flashDuration = 0.1f;
        
        private float _currentHealth;
        private Color _originalColor;
        private Renderer _playerRenderer;

        private void Awake()
        {
            _playerRenderer = GetComponent<Renderer>();
            _originalColor = _playerRenderer.material.color;
            _currentHealth = maxHealth;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0f)
            {
                Die();
            }
            else
            {
                StartCoroutine(FlashEffect());
            }
        }

        private void Die()
        {
            // Can be extended with death animation or game over logic
            Destroy(gameObject);
        }

        private IEnumerator FlashEffect()
        {
            _playerRenderer.material.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            _playerRenderer.material.color = _originalColor;
        }

        public float GetHealthPercentage()
        {
            return _currentHealth / maxHealth;
        }
    }
}
