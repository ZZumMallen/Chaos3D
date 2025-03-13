using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Chaos
{    
    public class BubbleGunProjectile : MonoBehaviour
    {
        public Ammo Ammo { get; set; }       
        
        private void Update()
        {
            
            Ray ray = new(transform.position, transform.forward);

            //Debug.Log(gameObject.layer);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Ammo.velocity * Time.deltaTime))
            {

                if (hit.collider.gameObject.layer != gameObject.layer)
                {
                    transform.position = hit.point;
                    if (hit.collider.gameObject.CompareTag("HostileTarget"))
                    {
                        hit.collider.SendMessage("ApplyBubble");
                        //Debug.Log("sent apply bubble");
                    }
                    else
                    {
                        hit.collider.SendMessage("ApplyDamage", Ammo.damage, SendMessageOptions.DontRequireReceiver);
                    }                        
                    GetComponent<MeshRenderer>().enabled = false;
                    Destroy(gameObject);
                    Destroy(this);

                }
            }
            else
            {
                transform.Translate(Vector3.forward * Ammo.velocity * Time.deltaTime);
                Destroy(gameObject,3);
                Destroy(this, 3);
            }
        }
    }
}

