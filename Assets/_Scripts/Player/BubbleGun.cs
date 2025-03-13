using UnityEngine;

namespace Chaos
{
    public class BubbleGun : MonoBehaviour
    {
        [SerializeField] private Ammo ammo;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firingPoint;

        //GameObject bubbleGunPrefab;
        SoundManager soundManager;

        private void Awake()
        {
            soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
            //bubbleGunPrefab = GameObject.FindGameObjectWithTag("Bubble");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                FireProjectile();
            }
        }

        void FireProjectile()
        {
            var projectile = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            soundManager.PlaySFX(soundManager.playerShoot);
            projectile.GetComponent<BubbleGunProjectile>().Ammo = ammo;            
        }
    }
}

