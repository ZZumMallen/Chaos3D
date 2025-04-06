using System;
using UnityEngine;

namespace Chaos
{
    public class NewUiManager : MonoBehaviour
    {
        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public abstract class Enemy
        {
            public static event Action<Enemy> OnDeath;

            protected virtual void Die()
            {
                OnDeath?.Invoke(this);
            }
        }
        
    }
}

