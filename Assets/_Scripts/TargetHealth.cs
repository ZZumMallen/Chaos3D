using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Chaos.Target
{
    public class TargetHealth : MonoBehaviour
    {
        [SerializeField] private GameObject _bubble;
        private Stats _stats;

        private readonly float _flashDuration = 0.1f;
        private Color _originalColor;
        private Renderer _targetRenderer;


        private float _currentHealth;
        private bool _initialized = false;

        public Stats Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                if (_stats != null && gameObject.activeInHierarchy)
                {
                    InitializeHealth();
                }
            }
        }

        void Awake()
        {
            _targetRenderer = GetComponent<Renderer>();
            if (_targetRenderer != null)
            {
                _originalColor = _targetRenderer.material.color;
            }

            if (!_initialized && _stats != null)
            {
                InitializeHealth();
            }

           
           
        }

        private void InitializeHealth()
        {
            _currentHealth = Stats.maxHealth;
            _initialized = true;
            ; Debug.Log($"Health initialized to {_currentHealth} for {gameObject.name}");
        }

        public void ApplyBubble()
        {
            Debug.Log("ApplyBubble Received");
            Instantiate(_bubble, gameObject.transform.position, _bubble.transform.rotation);
        }

        public void ApplyDamage(float damage)
        {
            if (!_initialized)
            {
                Debug.LogWarning("Health not initialized. Damage not applied.");
                return;
            }

            _currentHealth -= damage;
            ; Debug.Log($"Damage applied: {damage}. Current health: {_currentHealth}");

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
            if (_targetRenderer != null)
            {
                _targetRenderer.material.color = Color.white;
                yield return new WaitForSeconds(_flashDuration);
                _targetRenderer.material.color = _originalColor;
            }
            else
            {
                yield return null;
            }

        }



    }
}

