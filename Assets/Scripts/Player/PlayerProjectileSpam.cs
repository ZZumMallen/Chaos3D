using UnityEngine;
using UnityEngine.Events;
using Chaos.Enemies;



namespace Chaos.Player
{
    public class PlayerProjectileSpam : MonoBehaviour
    {
        [SerializeField] private bool _debugging = false;
        private float _velocity = 9f;
        private float _damage = 25f;

        public UnityEvent addCharge;
        

        private void Start()
        {
            addCharge.AddListener(GameObject.FindFirstObjectByType<PlayerWeaponController>().GetComponent<PlayerWeaponController>().HandleCharge);  
        }

        void Update()
        {
            transform.Translate(_velocity * Time.deltaTime * Vector3.forward);
            Destroy(gameObject, 3f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_debugging) Debug.Log("Trigger Entered: " + other.gameObject);

            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (_debugging) Debug.Log("TryGet Trigger: " + other.gameObject.name);
                addCharge.Invoke();
                enemy.ApplyDamage(_damage);

            }

            Destroy(gameObject);
        }

    }
}

