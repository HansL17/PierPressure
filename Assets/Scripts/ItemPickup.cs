using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] GameObject heldItem; // Reference to the currently held item
    [SerializeField] Transform itemAttachPoint; // Reference to the point where the item should be attached
    [SerializeField] GameObject tablePosition; // Reference to the specific GameObject on the table where the item should be placed
    private bool isPlacingItem; // Flag to indicate if the item is being placed

    private void Start()
    {
        // Find the item attach point as a child of the player
        itemAttachPoint = transform.Find("DishPlace");
        if (itemAttachPoint == null)
        {
            Debug.LogError("DishPlace not found! Make sure to create an empty GameObject childed to the player and name it 'DishPlace'.");
        }

        // Find the specific GameObject on the table where the item should be placed
        tablePosition = GameObject.Find("DishPosition");
        if (tablePosition == null)
        {
            Debug.LogError("DishPosition not found! Make sure to create an empty GameObject in the scene and name it 'DishPosition'.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object with the "Item" tag
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Dish"))
            {
                // Pick up the item if it's not already held
                if (heldItem == null)
                {
                    Debug.Log("Picking up item...");

                    // Store the reference to the held item
                    heldItem = hit.collider.gameObject;

                    // Disable the item's collider and rigidbody
                    Collider itemCollider = heldItem.GetComponent<Collider>();
                    if (itemCollider != null)
                        itemCollider.enabled = false;

                    Rigidbody itemRigidbody = heldItem.GetComponent<Rigidbody>();
                    if (itemRigidbody != null)
                        itemRigidbody.isKinematic = true;

                    // Attach the item to the item attach point
                    heldItem.transform.SetParent(itemAttachPoint);
                    heldItem.transform.localPosition = Vector3.zero;
                    heldItem.transform.localRotation = Quaternion.identity;
                }
            }
            // Check if the ray hits an object with the "Table" tag
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Table"))
            {
                // Place down the held item on the table if there is one
                if (heldItem != null)
                {
                    Debug.Log("Placing down item on table...");
                    isPlacingItem = true;

                    // Start the delay coroutine
                    StartCoroutine(PlaceItemWithDelay());
                }
            }
        }
    }

    private System.Collections.IEnumerator PlaceItemWithDelay()
    {
        yield return new WaitForSeconds(1.3f);

        // Check if the player is still placing the item
        if (isPlacingItem)
        {
            // Enable the item's collider and rigidbody
            Collider itemCollider = heldItem.GetComponent<Collider>();
            if (itemCollider != null)
                itemCollider.enabled = true;

            Rigidbody itemRigidbody = heldItem.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
                itemRigidbody.isKinematic = false;

            // Set the position of the held item to the table's position
            heldItem.transform.SetParent(null);
            heldItem.transform.position = tablePosition.transform.position;

            // Clear the reference to the held item
            heldItem = null;

            Debug.Log("Item placed on table!");
            isPlacingItem = false;
        }
    }
}

