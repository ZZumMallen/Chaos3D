using UnityEngine;

namespace Chaos
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] Ammo ammo;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform muzzle;

        void Start()
        {
            InvokeRepeating("fire", 1f, 1f);
        }

        void fire()
        {
            var projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            projectile.GetComponent<Projectile>().Ammo = ammo;
        }
    }
}