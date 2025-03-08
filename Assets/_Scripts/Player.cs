using UnityEngine;

namespace Chaos.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerHealth))]
    public class Player : MonoBehaviour
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
            // Process all input in Update
            _movement.ProcessInput();
            _jump.ProcessInput();
        }

        private void FixedUpdate()
        {
            // Apply physics in FixedUpdate
            _jump.HandleGroundCheck();
            _jump.HandleJump();
            _movement.Move();
            _movement.FlipPlayer();
        }

        // This public method allows other systems to damage the player
        public void ApplyDamage(float damage)
        {
            _health.ApplyDamage(damage);
        }
    }
}
