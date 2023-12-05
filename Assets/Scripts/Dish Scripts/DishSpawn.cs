using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSpawn : MonoBehaviour
{
    public GameObject dishPrefab;           // Reference to the dish prefab
    public float spawnDelay = 5f;           // Delay in seconds before spawning the dish
    private string buttonTag = "Button";    // Tag for button

    private bool isWaitingForSpawn;         // Flag to track if waiting for dish spawn
    private float spawnTimer;               // Timer for tracking the spawn delay
    public Transform dishSpawn;             //Waypoint for dishspawn
    public DishBar dishBar;                 //DishBar1

    private void Awake()
    {
        dishBar = GameObject.Find("DishBar").GetComponent<DishBar>(); //Get scriptz
    }

    private void Update()
    {
        if (isWaitingForSpawn)
        {
            // Update the spawn timer
            spawnTimer -= Time.deltaTime;

            // Check if the spawn delay has elapsed
            if (spawnTimer <= 0f)
            {
                // Spawn the dish next to the clicked button
                SpawnDish();

                // Reset the spawn delay variables
                isWaitingForSpawn = false;
                spawnTimer = 0f;
            }
        }
        else
        {
            // Check for left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Check if the ray hits a button game object
                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(buttonTag))
                {
                    // Start the spawn delay timer
                    isWaitingForSpawn = true;
                    spawnTimer = spawnDelay;
                    StartTimer();

                    // Log the timer start
                    Debug.Log("Spawn timer started!");
                }
            }
        }
    }

    private void SpawnDish()
    {
        // Instantiate the dish prefab next to the clicked button
        Vector3 spawnPosition = transform.position + transform.right;
        Quaternion spawnRotation = transform.rotation;
        GameObject dishSpawned = Instantiate(dishPrefab, dishSpawn.position, spawnRotation);

        float scale = 1.2f;
        dishSpawned.transform.localScale = new Vector3(scale, scale, scale);

        dishSpawned.tag = "Dish";

        BoxCollider boxCollider = dishSpawned.AddComponent<BoxCollider>();
        float width = 0.3042574f;
        float height = 0.07829487f;
        float depth = 0.289173f;
        boxCollider.center = new Vector3(0.01338816f, 0.02491823f, 0.01766568f);
        boxCollider.size = new Vector3(width, height, depth);
        
        // Log the dish spawn
        Debug.Log("Dish spawned!");
    }

    public void StartTimer()
    {
        StartCoroutine(dishBar.IncreaseDishBar());
    }
}
