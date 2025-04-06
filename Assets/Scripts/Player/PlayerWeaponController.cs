using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Chaos.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        Transform firingPoint;
        

        [SerializeField] private GameObject spammedProjectilePrefab;
        [SerializeField] private GameObject chargedProjectilePrefab;

        [SerializeField] private int _chargesRequired = 5;

        [SerializeField] private GameObject charge1;
        [SerializeField] private GameObject charge2;
        [SerializeField] private GameObject charge3;
        [SerializeField] private GameObject charge4;
        [SerializeField] private GameObject charge5;

        public int charge { get; private set; }

        SoundManager soundManager;
      
        private void Awake()
        {
            soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
            firingPoint = gameObject.transform.GetChild(0);
            charge1.SetActive(false); // TODO these all need to go into an iterable list
            charge2.SetActive(false);
            charge3.SetActive(false);
            charge4.SetActive(false);
            charge5.SetActive(false);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {                
                Instantiate(spammedProjectilePrefab, firingPoint.position, firingPoint.rotation); // TODO replace with unity pooling
                soundManager.PlaySFX(soundManager.playerShootSpam );
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (charge >= _chargesRequired)
                {
                    Instantiate(chargedProjectilePrefab, firingPoint.position, firingPoint.rotation);// TODO replace with unity pooling
                    soundManager.PlaySFX(soundManager.playerShootCharge);                    
                }
                
            }
        }

        public void HandleCharge()
        {
            charge++;

            if (charge == 1) charge1.SetActive(true);
            if (charge == 2) charge2.SetActive(true);
            if (charge == 3) charge3.SetActive(true);
            if (charge == 4) charge4.SetActive(true);
            if (charge == 5) charge5.SetActive(true);

        }

        public void ResetCharge()
        {
            charge = 0;
            charge1.SetActive(false); 
            charge2.SetActive(false);
            charge3.SetActive(false);
            charge4.SetActive(false);
            charge5.SetActive(false);
        }
    }
}