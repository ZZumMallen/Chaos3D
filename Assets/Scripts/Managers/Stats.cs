using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class Stats : ScriptableObject
{
    [Range(1f, 1000f)]
    [Tooltip("Normal range is between 100-400. Smaller values available for objects.")]
    public float maxHealth = 100f;


    public float meleeDamage = 1f;

    [Range(-5f,5f)]
    [Tooltip("Speed in meters per second. Negative is left, Positive is right.")]
    public float moveSpeed = 2f;

    public int pointValue = 10;
}
