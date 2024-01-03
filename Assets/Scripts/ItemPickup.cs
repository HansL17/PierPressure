using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
    public GameObject tBar1;
    public GameObject tBar2;
    public GameObject tBar3;
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
    public OrderTable1 orderT1;
    public OrderTable2 orderT2;
    public OrderTable3 orderT3;
    public CustomerMove cusMove;
    public SoundScript SFX;
    public SpawnCust cusSpawn;
    private Scene currentScene;


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
        orderT1 = GameObject.Find("DishPosition").GetComponent<OrderTable1>(); //Get script
        orderT2 = GameObject.Find("DishPosition2").GetComponent<OrderTable2>(); //Get script
        if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5")
        {orderT3 = GameObject.Find("DishPosition3").GetComponent<OrderTable3>();} else {orderT3 = null;}
        cusSpawn = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>();
        SFX = GameObject.Find("SoundDesign").GetComponent<SoundScript>();

        if(currentScene.name == "Tutorial Level" || currentScene.name == "Level2")
        {
            tBar3 = null;
        }
}

    private void Update()
    {   
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
                                tBar1.SetActive(true);
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
                                tBar2.SetActive(true);
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
                                if (tBar3 !=null){tBar3.SetActive(true);}
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
                player.SetDestination(trashcan.transform.position);
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

                if(whichTable.name == "T1_table"){
                    Canvas cT = tBar1.GetComponentInChildren<Canvas>();
                    Slider sT = cT.GetComponentInChildren<Slider>();
                    tBar script = sT.GetComponent<tBar>();
                    script.startDepletion = true;
                } else if(whichTable.name == "T2_table"){
                    Canvas cT = tBar2.GetComponentInChildren<Canvas>();
                    Slider sT = cT.GetComponentInChildren<Slider>();
                    tBar script = sT.GetComponent<tBar>();
                    script.startDepletion = true;
                } else if(whichTable.name == "T3_table"){
                    Canvas cT = tBar1.GetComponentInChildren<Canvas>();
                    Slider sT = cT.GetComponentInChildren<Slider>();
                    tBar script = sT.GetComponent<tBar>();
                    script.startDepletion = true;
                }
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
}

