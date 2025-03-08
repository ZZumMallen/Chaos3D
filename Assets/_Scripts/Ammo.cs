using UnityEngine;

public enum WeaponTriggerType
{
    MouseClick,
    KeyPress,
    RaycastHit,
    Auto
}

[CreateAssetMenu(fileName = "Ammo", menuName = "Scriptable Objects/Ammo")]
public class Ammo : ScriptableObject
{
    [Range(5f,50f)]
    [Tooltip("Bullet Velocity")]
    public float velocity = 8f;

    [Range(1f, 50f)]
    [Tooltip("Bullet Damage")]
    public float damage = 20f;

}
