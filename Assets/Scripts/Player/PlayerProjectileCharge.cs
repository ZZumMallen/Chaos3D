using UnityEngine;
using Chaos.Enemies;
using UnityEngine.Events;

namespace Chaos.Player
{
    public class PlayerProjectileCharge : MonoBehaviour
    {
        private float _velocity = 5f;
        private float _damage = 100f;

        public UnityEvent resetCharges;
     
        private void Start()
        {
            resetCharges.AddListener(GameObject.FindFirstObjectByType<PlayerWeaponController>().GetComponent<PlayerWeaponController>().ResetCharge);
            resetCharges.Invoke();
        }

        void Update()
        {
            transform.Translate(_velocity * Time.deltaTime * Vector3.forward);
            Destroy(gameObject, 3f);

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy)) //checks for enemy
            {                
                enemy.ApplyDamage(_damage);

            }
            
            if(other.gameObject.layer != LayerMask.NameToLayer("Player")) //Bullet should not be destroyed from contacting a player object
            {                
                Destroy(gameObject);               
            }            
        }
    }
}

