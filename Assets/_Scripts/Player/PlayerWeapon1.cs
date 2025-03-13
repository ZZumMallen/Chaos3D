using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Chaos
{
    public class PlayerWeapon1 : MonoBehaviour
    {
        [SerializeField] private Ammo Ammo;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firingPoint;

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
            projectile.GetComponent<Projectile1>().Ammo = Ammo;
        }

    }
}
