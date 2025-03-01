using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Chaos
{    
    public class Projectile : MonoBehaviour
    {
        private float damageValue = 20f;
        private void Awake()
        {
            Destroy(gameObject, 3);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Hostile"))
            {
                other.gameObject.SendMessage("TakeDamage", damageValue);
                Destroy(gameObject);
                Debug.Log("Target Hit:" + other.gameObject.name);

            }
            else
            {
                Destroy(gameObject);
                Debug.Log(other.gameObject.name);
            }
        }
    }
}

