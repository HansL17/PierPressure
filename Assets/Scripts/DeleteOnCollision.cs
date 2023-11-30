using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Collider component
        if (collision.collider != null)
        {
            // Destroy the other GameObject on collision
            Destroy(collision.collider.gameObject);
        }
    }
}
