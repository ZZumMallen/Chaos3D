using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Chaos
{
    public class WeaponEnemySighted : MonoBehaviour
    {
        [SerializeField] Ammo ammo;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private GameObject bulletPrefab;

        private LayerMask layermask;

        [SerializeField] private float shootCooldown = 1f;
        private float shootCooldownCounter;

        private void Start()
        {
            
            shootCooldownCounter = 0f;

            if (firingPoint == null)
            {
                Debug.LogError("firingPoint is not assigned in " + gameObject.name);
                return;
            }

            layermask = LayerMask.GetMask("Player");
            
        }

        private void Update()
        {
            Debug.DrawRay(firingPoint.position, firingPoint.forward * 10, Color.blue);
        }

        private void FixedUpdate()
        {
            if (firingPoint != null && Physics.Raycast(firingPoint.position, firingPoint.TransformDirection(Vector3.forward), out _, 100, layermask) && shootCooldownCounter < 0)
            {
                EnemyShoot();
                //Debug.Log("Shoot");
                shootCooldownCounter = shootCooldown;
            }
            else
            {
                shootCooldownCounter -= Time.fixedDeltaTime;
            }

        }

        void EnemyShoot()
        {
            if (firingPoint == null || bulletPrefab == null)
            {
                Debug.LogError("Missing references: firingPoint or bulletPrefab");
                return;
            }

            var projectile = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            projectile.GetComponent<EnemyProjectile>().Ammo = ammo;

        }

    }
}

