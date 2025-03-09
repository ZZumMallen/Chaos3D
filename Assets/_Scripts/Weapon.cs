using System.Collections;
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

        SoundManager soundManager;

        private void Awake()
        {
            soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        }

        private void Update()
        {      
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FireProjectile();
            }          
        }

        void FireProjectile()
        {
            var projectile = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            soundManager.PlaySFX(soundManager.playerShoot);
            projectile.GetComponent<Projectile>().Ammo = ammo;         
        }
    }
}
