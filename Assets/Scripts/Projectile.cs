using UnityEngine;
using UnityEngine.Rendering;


namespace Chaos
{
    public class Projectile : MonoBehaviour
    {
        public Ammo Ammo { get; set; }

        void Update()
        {
            if (Physics.Raycast(new Ray(transform.position, transform.right), out RaycastHit hit, 40))
            {
                
                transform.position = hit.point;
                hit.collider.SendMessage("ApplyDamage", Ammo.KineticEnergyDamage, SendMessageOptions.DontRequireReceiver);
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, 1f);
                Destroy(this);
            }
            else
            {
                transform.Translate(Vector3.right * Ammo.velocity * Time.deltaTime);
                Destroy(this, 1f);
            }
        }
    }
}