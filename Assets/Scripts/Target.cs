using UnityEngine;

public class Target : MonoBehaviour
{
    private Renderer rd;
    [SerializeField] float health = 1000f;

    private void Start()
    {
        rd = GetComponent<Renderer>();
    }

    void ApplyDamage(float damage)
    {
        Debug.Log($"Applying {damage} damage");

        health -= damage;

        if (health <= 0)
        {
            Debug.Log("No health left, please destroy");
            Destroy(gameObject);
        }

        rd.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
