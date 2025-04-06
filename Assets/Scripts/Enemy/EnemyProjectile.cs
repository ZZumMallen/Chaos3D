using Chaos.Player;
using UnityEngine;

namespace Chaos
{
    public class EnemyProjectile : MonoBehaviour
    {
        public Ammo Ammo { get; set; } 

        private void FixedUpdate()
        {
            Ray ray = new(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 50))
            {

                if (hit.collider.gameObject.TryGetComponent(out PlayerHealth _playerHealth ))
                {                  
                    _playerHealth.HandleDamage(Ammo.damage);
                    GetComponent<MeshRenderer>().enabled = false;
                    Destroy(gameObject);
                    Destroy(this);
                }
            }
            else
            {
                transform.Translate(Ammo.velocity * Time.deltaTime * Vector3.forward);
            }
        }
    }
}

