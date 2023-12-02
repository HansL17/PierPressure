using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] GameObject heldItem; // Reference to the currently held item
    [SerializeField] Transform itemAttachPoint; // Reference to the point where the item should be attached
    [SerializeField] GameObject DishLoc; // Reference to the Dish Location on the counter
    [SerializeField] GameObject tablePosition; // Reference to the specific GameObject on the table where the item should be placed
    [SerializeField] GameObject whichTable;
    public GameObject dishInT1;
    public GameObject dishInT2;
    public GameObject dishInT3;
    private bool isPlacingItem; // Flag to indicate if the item is being placed
    private bool isMovingToDestination = false;
    public bool table1Placed = false; // Flag to indicate if item is placed on Table 
    public bool table2Placed = false;
    public bool table3Placed = false; 


    private GameObject playerObject;
    private NavMeshAgent player; // Reference to Player NavMeshAgent (Wendy)

    public Scoring scores;
    public TableBar tBar;
    public Order order;
    public CustomerMove cusMove;
    public SoundScript SFX;
    public SpawnCust cusSpawn;


    private void Start()
    {
        //Get the name of the scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Find the item attach point as a child of the player
        itemAttachPoint = transform.Find("DishPlace");
        if (itemAttachPoint == null)
        {
            Debug.LogError("DishPlace not found! Make sure to create an empty GameObject childed to the player and name it 'DishPlace'.");
        }

        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<NavMeshAgent>(); //Get Player NavMeshAgent
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        scores = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get script
        tBar = GameObject.Find("CustomerLine").GetComponent<TableBar>(); //Get script
        order = GameObject.Find("DishPosition").GetComponent<Order>(); //Get script
        cusSpawn = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>();
        SFX = GameObject.Find("SoundDesign").GetComponent<SoundScript>();

    }

    private void Update()
    {
        if (isMovingToDestination)
        {
            if (player.pathStatus == NavMeshPathStatus.PathComplete && player.remainingDistance <= player.stoppingDistance)
            {
                SFX.DishSound();
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
                        if (order.order1Spawned)
                        {
                            isPlacingItem = true;
                            table1Placed = true;
                            Action2Done();
                            Debug.Log("Placing down item on table...");
                            
                            dishInT1 = heldItem;
                            order.order1Spawned = false;
                            cusMove.t1_occupied = false;
                        }
                    }
                    else if (whichTable.name == "T2_table")
                    {
                        tablePosition = GameObject.Find("DishPosition2");
                        if (order.order2Spawned)
                        {
                            isPlacingItem = true;
                            table2Placed = true;
                            Action2Done();
                            Debug.Log("Placing down item on table...");
                            dishInT2 = heldItem;
                            order.order2Spawned = false;
                            cusMove.t2_occupied = false;
                        }
                    } else if (whichTable.name == "T3_table")
                    {
                        tablePosition = GameObject.Find("DishPosition3");
                        if (order.order3Spawned)
                        {
                            isPlacingItem = true;
                            table3Placed = true;
                            Action2Done();
                            Debug.Log("Placing down item on table...");
                            dishInT3 = heldItem;
                            order.order3Spawned = false;
                            cusMove.t3_occupied = false;
                        }
                    }else isPlacingItem = false;

                    if (tablePosition == null)
                    {
                        Debug.Log("DishPosition not found! Make sure to create an empty GameObject in the scene and name it 'DishPosition'.");
                    }                 
                    
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

            // Clear the reference to the held item
            heldItem = null;

            Debug.Log("Item placed on table!");
            isPlacingItem = false;
        }
    }

    private void Action2Done()
    {
        scores.isAction1Done = false;
        scores.isAction2Done = true;
        scores.consecutiveActions1 = 0;
        scores.consecutiveActions2++;
        Debug.Log("Action 2 Done");
        scores.AddScore();
    }

 
}

