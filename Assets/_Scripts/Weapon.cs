using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Chaos
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] Ammo ammo;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private GameObject bulletPrefab;       

        private void Update()
        {
            HandleInput();
            //Debug.DrawRay(firingPoint.position, firingPoint.forward * 10, Color.blue);
        }

        void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            var projectile = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            projectile.GetComponent<Projectile>().Ammo = ammo;
            
        }
    }
}
