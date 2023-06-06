using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] GameObject heldItem; // Reference to the currently held item
    [SerializeField] Transform itemAttachPoint; // Reference to the point where the item should be attached
    [SerializeField] GameObject DishLoc; // Reference to the Dish Location on the counter
    [SerializeField] GameObject tablePosition; // Reference to the specific GameObject on the table where the item should be placed
    [SerializeField] GameObject whichTable;
    private bool isPlacingItem; // Flag to indicate if the item is being placed
    private bool isMovingToDestination = false;
    public bool dish1placed = false;
    public bool dish2placed = false;

    private GameObject playerObject;
    private NavMeshAgent player; // Reference to Player NavMeshAgent

    public Scoring scores;

    private void Start()
    {
        // Find the item attach point as a child of the player
        itemAttachPoint = transform.Find("DishPlace");
        if (itemAttachPoint == null)
        {
            Debug.LogError("DishPlace not found! Make sure to create an empty GameObject childed to the player and name it 'DishPlace'.");
        }

        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<NavMeshAgent>(); //Get Player NavMeshAgent
        scores = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get script
    }

    private void Update()
    {
        if (isMovingToDestination)
        {
            if (player.pathStatus == NavMeshPathStatus.PathComplete && player.remainingDistance <= player.stoppingDistance)
            {
                Debug.Log("Picking up item...");
                

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

                isMovingToDestination = false;
            }
        }
        else
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
                    // Store the reference to the held item
                    heldItem = hit.collider.gameObject;
                    
                    DishLoc = GameObject.Find("DishSpawn");
                    player.SetDestination(new Vector3(DishLoc.transform.position.x, player.transform.position.y, DishLoc.transform.position.z));
                    isMovingToDestination = true;
                }
            }
            // Check if the ray hits an object with the "Table" tag
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Table"))
            {
                // Place down the held item on the table if there is one
                if (heldItem != null)
                {
                    // Find the specific GameObject on the table where the item should be placed
                    whichTable = hit.collider.gameObject;
                    if (whichTable.name == "T1_table")
                    {
                        tablePosition = GameObject.Find("DishPosition");
                        dish1placed = true;
                    }
                    else if (whichTable.name == "T2_table")
                    {
                        tablePosition = GameObject.Find("DishPosition2");
                    }

                    if (tablePosition == null)
                    {
                        Debug.LogError("DishPosition not found! Make sure to create an empty GameObject in the scene and name it 'DishPosition'.");
                    }

                    Debug.Log("Placing down item on table...");
                    isPlacingItem = true;
                    scores.AddScore(10);

                    // Start the delay coroutine
                    StartCoroutine(PlaceItemWithDelay());
                }
            }
        }
    }}

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
            dish1placed = true;

            // Clear the reference to the held item
            heldItem = null;

            Debug.Log("Item placed on table!");
            isPlacingItem = false;
        }
    }
}

