using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Chaos
{
    public class Weapon : MonoBehaviour
    {

        [SerializeField] private Transform firingPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;

        //reference doc in the python folder
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
            var bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = firingPoint.forward * bulletSpeed;

        }
    }
}
