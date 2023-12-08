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
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public GameObject dishInT1;
    public GameObject dishInT2;
    public GameObject dishInT3;
    public GameObject trashcan;
    private bool isPlacingItem; // Flag to indicate if the item is being placed
    public bool isHoldingItem;
    private bool isMovingToDestination = false;
    public bool OnWaypoint = false;
    public bool OnTrash = false;
    public bool table1Placed = false; // Flag to indicate if item is placed on Table 
    public bool table2Placed = false;
    public bool table3Placed = false; 
    public bool go = false;

    private string ordName = "";

    private GameObject playerObject;
    private NavMeshAgent player; // Reference to Player NavMeshAgent (Wendy)

    public Scoring scores;
    public TableBar tBar;
    public OrderTable1 orderT1;
    public OrderTable2 orderT2;
    public OrderTable3 orderT3;
    public CustomerMove cusMove;
    public SoundScript SFX;
    public SpawnCust cusSpawn;
    public T1Pathfind t1pf;
    public T2Pathfind t2pf;
    public T3Pathfind t3pf;


    private void Start()
    {
        //Get the name of the scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Find the item attach point as a child of the player
        itemAttachPoint = transform.Find("DishPlace");
        if (itemAttachPoint == null)
        {
            Debug.Log("DishPlace not found! Make sure to create an empty GameObject childed to the player and name it 'DishPlace'.");
        }

        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<NavMeshAgent>(); //Get Player NavMeshAgent
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        scores = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get script
        tBar = GameObject.Find("CustomerLine").GetComponent<TableBar>(); //Get script
        orderT1 = GameObject.Find("DishPosition").GetComponent<OrderTable1>(); //Get script
        orderT2 = GameObject.Find("DishPosition2").GetComponent<OrderTable2>(); //Get script
        if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5")
        {orderT3 = GameObject.Find("DishPosition3").GetComponent<OrderTable3>();
        t3pf = GameObject.Find("T3_table").GetComponent<T3Pathfind>();} else {orderT3 = null; t3pf = null;}
        cusSpawn = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>();
        SFX = GameObject.Find("SoundDesign").GetComponent<SoundScript>();
        t1pf = GameObject.Find("T1_table").GetComponent<T1Pathfind>();
        t2pf = GameObject.Find("T2_table").GetComponent<T2Pathfind>();
        

    }

    private void Update()
    {   
        if(go == true){
            if (!player.pathPending && player.remainingDistance <= player.stoppingDistance)
            {
                if (!player.hasPath || player.velocity.sqrMagnitude == 0f)
                {
                StartCoroutine(WaitAndMoveToNextWaypoint());
                }
            }
        }

        //if (heldItem != null) {Debug.Log(heldItem.name);}
        if (isMovingToDestination)
        {
            if (player.pathStatus == NavMeshPathStatus.PathComplete && player.remainingDistance <= player.stoppingDistance)
            {
                SFX.DishSound();
                Debug.Log("Picking up item...");

                StartCoroutine(GettingItem());
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
                    OnWaypoint = true;
                    t1pf.OnTable = false;
                    t2pf.OnStart = false;
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
                        if (orderT1.order1Spawned)
                        {
                            if (orderT1.OrderNum == 1)
                            {
                                ordName = "dish1(Clone)";
                            } else if (orderT1.OrderNum == 2)
                            {
                                ordName = "dish2(Clone)";
                            }
                            dishInT1 = heldItem;
                            if (heldItem.name == ordName){
                                isPlacingItem = true;
                                table1Placed = true;
                                Action2Done();
                                Debug.Log("Placing down item on table...");
                                orderT1.order1Spawned = false;
                                cusMove.t1_occupied = false;
                            } else {Debug.Log("Not the right order");}
                        }
                    }
                    else if (whichTable.name == "T2_table")
                    {
                        tablePosition = GameObject.Find("DishPosition2");
                        if (orderT2.order2Spawned)
                        {
                            if (orderT2.OrderNum == 1)
                            {
                                ordName = "dish1(Clone)";
                            } else if (orderT2.OrderNum == 2)
                            {
                                ordName = "dish2(Clone)";
                            }
                            dishInT2 = heldItem;
                            if (heldItem.name == ordName){
                                isPlacingItem = true;
                                table2Placed = true;
                                Action2Done();
                                Debug.Log("Placing down item on table...");
                                orderT2.order2Spawned = false;
                                cusMove.t2_occupied = false;
                            } else {Debug.Log("Not the right order");}
                        }
                    } else if (whichTable.name == "T3_table")
                    {
                        tablePosition = GameObject.Find("DishPosition3");
                        if (orderT3.order3Spawned)
                        {
                            if (orderT3.OrderNum == 1)
                            {
                                ordName = "dish1(Clone)";
                            } else if (orderT3.OrderNum == 2)
                            {
                                ordName = "dish2(Clone)";
                            }
                            dishInT3 = heldItem;
                            if (heldItem.name == ordName){
                                isPlacingItem = true;
                                table3Placed = true;
                                Action2Done();
                                Debug.Log("Placing down item on table...");
                                orderT3.order3Spawned = false;
                                cusMove.t3_occupied = false;
                            } else {Debug.Log("Not the right order");}
                        }
                    }else isPlacingItem = false;

                    if (tablePosition == null)
                    {
                        Debug.Log("DishPosition not found! Make sure to create an empty GameObject in the scene and name it 'DishPosition'.");
                    }                 
                    
                    if (heldItem.name == ordName){
                    // Start the delay coroutine
                    StartCoroutine(PlaceItemWithDelay());
                    } else {
                        Debug.Log("no");
                    }
                }
            } else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Trash"))
            {
                OnTrash = true;
                t2pf.OnStart = false;

                if (t1pf.OnTable == true)
                {
                    MoveToNextWaypoint();
                    go = true;
                    t1pf.OnTable = false;
                }
                else if (t3pf.fromB == true)
                {
                    MoveToNextWaypoint();
                    go = true;
                    t3pf.fromB = false;
                } else {player.SetDestination(trashcan.transform.position);}
                StartCoroutine(ThrowingDish());
            }
        }
        if (heldItem == null)
        {
            isHoldingItem = false;
        }
    }}

    private System.Collections.IEnumerator PlaceItemWithDelay()
    {
        while (player.pathPending || player.remainingDistance > player.stoppingDistance)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0f);

        // Check if the player is still placing the item
        if (isPlacingItem)
        {
            if (heldItem != null)
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
                isHoldingItem = false;
            }
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

    private IEnumerator ThrowingDish() {
        while (player.pathPending || player.remainingDistance > player.stoppingDistance)
        {
            yield return null;
        }
        Destroy(heldItem);
        isHoldingItem = false;
    }

    private IEnumerator GettingItem() {
        while (player.pathPending || player.remainingDistance > player.stoppingDistance)
        {
            yield return null;
        }
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

                isHoldingItem = true;
                isMovingToDestination = false;
    }

    private void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            player.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex++;
        }
        else
        {
            go = false;
            currentWaypointIndex = 0;
        }
    }

    private System.Collections.IEnumerator WaitAndMoveToNextWaypoint()
    {
        yield return new WaitForSeconds(0f);
        MoveToNextWaypoint();
    }

}

