using System.Collections;
using Chaos.Enemies;
using Chaos;
using UnityEngine;

namespace Chaos.BubbleSystem
{
    public class BubbleProjectileController : MonoBehaviour
    {
       
        //[SerializeField] private float lifespan = 4f;
        [SerializeField] private float _velocity = 10f; // Fallback velocity
        //[SerializeField] private float _damage = 0f;

        [SerializeField] private float _lifetimeSeconds = 4f;
        private Coroutine _lifetimeCoroutine;
        private bool _isActive = false;




        private void OnEnable()
        {
            _isActive = true;
            _lifetimeCoroutine = StartCoroutine(DeactivateAfterLifetime());
        }

        private void OnDisable()
        {
            _isActive = false;
            if (TryGetComponent<MeshRenderer>(out var renderer))
            {
                renderer.enabled = true;
            }
        }

        private void FixedUpdate()
        {

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, _velocity * Time.deltaTime))
            {
                if (hit.collider.gameObject.TryGetComponent(out Enemy _))
                {
                    CaptureEnemy(hit.collider.gameObject);
                    Deactivate();
                }

                else
                {
                    transform.Translate(_velocity * Time.deltaTime * Vector3.forward);
                    Deactivate();
                }  
            }

            else
            {
                transform.Translate(_velocity * Time.deltaTime * Vector3.forward);
                Deactivate();
            }
        }



        private void Deactivate()
        {
            if (_lifetimeCoroutine != null)
            {
                StopCoroutine(_lifetimeCoroutine);
                _lifetimeCoroutine = null;
            }



        }

        private IEnumerator DeactivateAfterLifetime()
        {
            yield return new WaitForSeconds(_lifetimeSeconds);
            if (_isActive)
            {
                Deactivate();
            }
        }

        private void CaptureEnemy(GameObject enemy)
        {

            if (enemy == null)
            {
                Debug.Log("enemyNull");
                return;
            }
            

            
        }
    }
}