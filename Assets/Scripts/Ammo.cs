using UnityEngine;


namespace Chaos
{
    [CreateAssetMenu(fileName = "Ammo", menuName = "Scriptable Objects/Ammo")]
    public class Ammo : ScriptableObject
    {
        [Range(1, 500)]
        public float damage = 10;

        [Range(0f, 2f)]
        [Tooltip("Percentage")]
        public float bonusDamagePercent = 1;

        [Range(1f, 5f)]
        public float projectileSize = 1;
        
        public float velocity = 50;

        public float KineticEnergyDamage { get => (damage + damage * (bonusDamagePercent/100)); }
        //public float KineticEnergyDamage { get => (mass * Mathf.Pow(velocity, 2)/2); }
    }
}