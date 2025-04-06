using UnityEngine;

namespace Chaos.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _movement;
        private PlayerJump _jump;
        private PlayerHealth _health;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _jump = GetComponent<PlayerJump>();
            _health = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            
            _movement.ProcessInput();
            _jump.ProcessInput();
        }

        public void HandleDamage(float damage)
        {
            _health.HandleDamage(damage);
        }

        private void FixedUpdate()
        {
            
            _jump.HandleGroundCheck();
            _jump.HandleJump();
            _movement.Move();
            _movement.FlipPlayer();
        }

    }
}
