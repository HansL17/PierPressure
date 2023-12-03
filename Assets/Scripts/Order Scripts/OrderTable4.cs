// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// using UnityEngine.SceneManagement;

// public class OrderTable4 : MonoBehaviour
// {
//     public CustomerMove cusMove; //Reference to customer move script
//     public ItemPickup itemPick; // Reference to item pick up script
//     [SerializeField] NavMeshAgent player; // Reference to Player NavMeshAgent

//     public Sprite[] Orders;

//     private bool isTimerActive = false;
//     public bool order4Spawned = false;
//     public float timer4Duration = 5f;
//     public float timer4;

//     public GameObject dishPos4;

//     GameObject orderSprite;
//     GameObject[] orderSprites;
    
//     public bool isTable4Placed;

//     private Scene currentScene;

//     void Start()
//     {
//         cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); //Get script
//         itemPick = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get script

//         currentScene = SceneManager.GetActiveScene();        
//     } 

//     void Update()
//     {
//         isTable4Placed = itemPick.table4Placed;

//             if (cusMove.t4_occupied && order4Spawned == false)
//             {
//                 // Start the timer
//                 if (!isTimerActive)
//                 {
//                     timer4 = timer4Duration;
//                     isTimerActive = true;
//                 }

//                 // Update the timer
//                 timer4 -= Time.deltaTime;

//                 // Check if the timer has completed
//                 if (timer4 <= 0f && order4Spawned == false)
//                 {
//                     order4Spawned = true;
//                     orderSprite = new GameObject("OrderSprite4");
//                     orderSprite.tag = "OrderSprite4";
//                     SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                    
//                     float randomValue = Random.value;
//                     if (randomValue < 0.5f) {spriteRenderer.sprite = Orders[0];}
//                     else {spriteRenderer.sprite = Orders[1];}
                    
//                     orderSprite.transform.position = new Vector3 (dishPos4.transform.position.x, dishPos4.transform.position.y + 0.7f, dishPos4.transform.position.z);
//                     orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
//                     timer4 = 0f;
//                     isTimerActive = false;
//                 } 
//             }
        
//         if (isTable4Placed && player.velocity.magnitude < 0.1f)
//         {
//             orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite4");
//             Debug.Log("Destroyed");
//             foreach (GameObject obj in orderSprites)
//             {
//                 Destroy(obj);
//             }
//             itemPick.table4Placed = false;
//         }
//     }

//     public void ResetTable()
//     {
//         orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite4");
//         Debug.Log("Destroyed");
//         foreach (GameObject obj in orderSprites)
//         {
//         Destroy(obj);
//         } 
//         order4Spawned = false;
//         cusMove.t4_occupied = false;
//         timer4 = 0f;
//         isTimerActive = false;
//     }
// }
