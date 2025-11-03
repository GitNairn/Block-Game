using UnityEngine;

public class KnockBackCubeScript : MonoBehaviour
{
    public float knockbackForce = 10f; // how strong the push should be
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            // Get direction from cube to the object
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            // Apply force in that direction
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
