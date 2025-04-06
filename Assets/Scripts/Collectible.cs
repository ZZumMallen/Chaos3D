using System;
using UnityEngine;

// Attach this to any collectible item prefab to

namespace Chaos
{
    public class Collectible : MonoBehaviour
    {

        [SerializeField] int points = 10;
        Collider _coll;

        public static event Action<int> OnAnyCollected;

        private void Awake()
        {
            _coll = GetComponent<Collider>();
        }

        private void Start()
        {
            if (!_coll.isTrigger)
            {
                _coll.isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnAnyCollected?.Invoke(points);
                gameObject.SetActive(false);
             
            }
        }
    }
    
}

