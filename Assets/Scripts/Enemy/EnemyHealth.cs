using System;
using System.Collections;
using System.Collections.Generic;
using Chaos.UI;
using UnityEngine;

namespace Chaos.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public Action<float> EnemyHit;

        private Stats _stats;

        private Renderer _renderer;
        private Color _originalColor;

        private Transform _transform;
        private Vector3 _originalSize;
        
        private UIManager _uiManager;
        private int _pointValue;
        private readonly float _flashDuration = 0.1f;

        private float _currentHealth;
        private bool _initialized = false;

        enum NullReport
        {
            uimanager,
            stats,
            initialized
        }


        private Enemy _enemyComponent;   

        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            if (_renderer != null)
            {
                _originalColor = _renderer.sharedMaterial.color;
            }



            _enemyComponent = GetComponent<Enemy>(); 

            _transform = GetComponent<Transform>();
            if(_transform != null)
            {
                _originalSize = _transform.localScale;
            }
           
        }

        private void Start()
        {
            _uiManager = FindFirstObjectByType<UIManager>();
            if (_uiManager == null) Tattle(NullReport.uimanager);
        }

        public void SetStats(Stats stats)
        {
            _stats = stats;
            if (_stats != null)
            {
                _pointValue = _stats.pointValue;
                InitializeHealth();
            }
        }

        private void InitializeHealth()
        {
            if (_stats != null)
            {
                _currentHealth = _stats.maxHealth;
                _initialized = true;
            }
            else
            {
                Tattle(NullReport.stats);
                _currentHealth = 100f; // Default value
                _initialized = true;
            }
            
        }

        public void HandleDamage(float damage)
        {
            if (!_initialized)
            {
                Tattle(NullReport.initialized);
                return;
            }

            EnemyHit?.Invoke(damage);

            _currentHealth -= damage;

            //Debug.Log("Damage Taken: " + damage);

            if (_currentHealth <= 0f)
            {               
                Die();
            }
            else
            {
                StartCoroutine(FlashEffect());
                StartCoroutine(PulseEffect());
            }
        }

        private void Die()
        {

            if (_uiManager != null)
            {
                // Get the point value either from stats or from Enemy component
                int points = (_enemyComponent != null) ? _enemyComponent.GetPointValue() : _pointValue;
                _uiManager.OnEnemyDeath(points);
            }

            Destroy(gameObject);

        }

        private IEnumerator FlashEffect()
        {            
            _renderer.sharedMaterial.color = Color.white;
            yield return new WaitForSeconds(_flashDuration);
            _renderer.sharedMaterial.color = _originalColor;
        }

        private IEnumerator PulseEffect()
        {
            _transform.localScale *= 1.2f;
            yield return new WaitForSeconds(_flashDuration);
            _transform.localScale = _originalSize;
        }


        private void Tattle(NullReport _item)
        {
            switch (_item)
            {
                case NullReport.uimanager: 
                    Debug.LogError("UIManager not found in scene. Score updates will not work.", this); 
                    break;

                case NullReport.stats:
                    Debug.LogWarning("Trying to initialize health with null stats.", this);
                    break;

                case NullReport.initialized:
                    Debug.LogWarning("Health not initialized. Damage not applied.", this);
                    break;

            }         
            
        }
    }
}

