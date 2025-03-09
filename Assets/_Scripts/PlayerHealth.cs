using System.Collections;
using UnityEngine;

namespace Chaos.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 1000f;

        private readonly float _flashDuration = 0.1f;        
        private float _currentHealth;
        private Color _originalColor;
        private Renderer _playerRenderer;

        SoundManager soundManager;

        private void Awake()
        {
            _playerRenderer = GetComponent<Renderer>();
            _originalColor = _playerRenderer.material.color;
            _currentHealth = maxHealth;
            soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
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
            soundManager.PlaySFX(soundManager.playerDeath);
            Destroy(gameObject);
        }

        private IEnumerator FlashEffect()
        {
            _playerRenderer.material.color = Color.white;
            yield return new WaitForSeconds(_flashDuration);
            _playerRenderer.material.color = _originalColor;
        }

        public float GetHealthPercentage()
        {
            return _currentHealth / maxHealth;
        }
    }
}
