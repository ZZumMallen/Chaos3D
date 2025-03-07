using UnityEngine;

namespace Chaos
{
    public class EnemyProjectile : MonoBehaviour
    {
        public Ammo Ammo { get; set; }
        private string layerString;


        // other.gameObject.layer == LayerMask.NameToLayer("Player")

        private void Start()
        {
            if (Ammo.isEnemyBullet)
            {
                layerString = "Hostile";
            }
            else
            {
                layerString = "Player";
            }
        }

        private void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, Ammo.velocity * Time.deltaTime))
            {

                if (hit.collider.gameObject.layer != LayerMask.NameToLayer(layerString))
                {
                    transform.position = hit.point;
                    hit.collider.SendMessage("ApplyDamage", Ammo.damage, SendMessageOptions.DontRequireReceiver);
                    GetComponent<MeshRenderer>().enabled = false;
                    Destroy(gameObject);
                    Destroy(this);
                }
            }
            else
            {
                transform.Translate(Vector3.forward * Ammo.velocity * Time.deltaTime);
            }
        }
    }
}

