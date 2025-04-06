using UnityEngine;
using Chaos.Player;

namespace Chaos
{
    public class PlayerHazard : MonoBehaviour
    {
        [SerializeField] private float _spikeDamage;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out PlayerController _player))
            {
                _player.HandleDamage(_spikeDamage);
            }
            
        }

    }
}

