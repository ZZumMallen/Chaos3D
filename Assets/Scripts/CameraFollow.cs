using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float followSpeed = 5f;
    [Tooltip("Leash must be negative")]
    [Range(-20, 0)]
    [SerializeField] private float leash = -10f;
    [SerializeField] Vector3 offset = new(0, 2, -5);

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        if (targetPosition.y > leash)
        {   
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

            
    }
}
