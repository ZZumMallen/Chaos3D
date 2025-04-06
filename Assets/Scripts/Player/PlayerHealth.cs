using System.Collections;
using Chaos.UI;
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
        UIManager uiManager;
        

        private void Awake()
        {
            _playerRenderer = GetComponent<Renderer>();
            _originalColor = _playerRenderer.sharedMaterial.color;
            _currentHealth = maxHealth;
            soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
            uiManager = FindFirstObjectByType<UIManager>();
        }

        public void HandleDamage(float damage)
        {
            var _initialDamage = damage;
            
            _currentHealth -= damage;
            Debug.Log("Player Damage Taken: " + _initialDamage);
            Debug.Log("Player Health: " + _currentHealth + "/" + maxHealth);

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
            uiManager.OnLoseConditionsMet();
            Destroy(gameObject);
        }

        private IEnumerator FlashEffect()
        {
            soundManager.PlaySFX(soundManager.playerDamage);
            _playerRenderer.sharedMaterial.color = Color.white;
            yield return new WaitForSeconds(_flashDuration);
            _playerRenderer.sharedMaterial.color = _originalColor;
        }

    }
}
