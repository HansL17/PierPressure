using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{

    private string cusTag = "Customer";
    private string HcusTag = "HighCustomer";

    public SpawnCust cusSpawn;

    private void Awake()
    {
        cusSpawn = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Customer Collided");

        // Check if the colliding object has a CustomerTag
        if (other.gameObject.CompareTag(cusTag) || other.gameObject.CompareTag(HcusTag))
        {
            // Destroy the other GameObject on collision
            Destroy(other.gameObject);
            cusSpawn.cusCount--;
        }
    }
}
