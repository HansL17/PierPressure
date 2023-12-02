using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Order : MonoBehaviour
{
    public CustomerMove cusMove; //Reference to customer move script
    public ItemPickup itemPick; // Reference to item pick up script
    [SerializeField] NavMeshAgent player; // Reference to Player NavMeshAgent

    public Sprite order;

    private bool isTimer1Active = false;
    private bool isTimer2Active = false;
    private bool isTimer3Active = false;
    public bool order1Spawned = false;
    public bool order2Spawned = false;
    public bool order3Spawned = false;
    public float timer1Duration = 5f;
    public float timer2Duration = 7f;
    public float timer3Duration = 7f;
    public float timer1;
    public float timer2;
    public float timer3;

    GameObject orderSprite;
    GameObject[] orderSprites;
    
    public bool isTable1Placed;
    public bool isTable2Placed;
    public bool isTable3Placed;

    void Start()
    {
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); //Get script
        itemPick = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get script
    } 

    void Update()
    {
               
        isTable1Placed = itemPick.table1Placed;
        isTable2Placed = itemPick.table2Placed;
        isTable3Placed = itemPick.table3Placed;

        if (cusMove.t1_occupied && order1Spawned == false)
        {
            // Start the timer
            if (!isTimer1Active)
            {
                timer1 = timer1Duration;
                isTimer1Active = true;
            }

            // Update the timer
            timer1 -= Time.deltaTime;

            // Check if the timer has completed
            if (timer1 <= 0f && order1Spawned == false)
            {
                order1Spawned = true;
                orderSprite = new GameObject("OrderSprite1");
                orderSprite.tag = "OrderSprite1";
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3(25.1922302f, 1.7f, 4.51000023f); 
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                timer1 = 0f;
                isTimer1Active = false;
            } 
        }
        
        if (cusMove.t2_occupied && order2Spawned == false)
        {
            // Start the timer
            if (!isTimer2Active)
            {
                timer2 = timer2Duration;
                isTimer2Active = true;
            }

            // Update the timer
            timer2 -= Time.deltaTime;

            // Check if the timer has completed
            if (timer2 <= 0f && order2Spawned == false)
            {
                order2Spawned = true;
                GameObject orderSprite = new GameObject("OrderSprite2");
                orderSprite.tag = "OrderSprite2";
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3(29.3195457f, 1.7f, 4.59800053f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                timer2 = 0f;
                isTimer2Active = false;
            }
        }

        if (cusMove.t3_occupied && order3Spawned == false)
        {
            // Start the timer
            if (!isTimer3Active)
            {
                timer3 = timer3Duration;
                isTimer3Active = true;
            }

            // Update the timer
            timer3 -= Time.deltaTime;

            // Check if the timer has completed
            if (timer3 <= 0f && order3Spawned == false)
            {
                order3Spawned = true;
                GameObject orderSprite = new GameObject("OrderSprite3");
                orderSprite.tag = "OrderSprite3";
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3 (27.0580006f, 1.7f, 2.08400011f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                timer3 = 0f;
                isTimer3Active = false;
            }
        }
        
        if (isTable1Placed && player.velocity.magnitude < 0.1f)
        {
            orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite1");
            Debug.Log("Destroyed");
            foreach (GameObject obj in orderSprites)
            {
                Destroy(obj);
            }
            itemPick.table1Placed = false;
        }
        
        if (isTable2Placed && player.velocity.magnitude < 0.1f)
        {
            orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite2");
            Debug.Log("Destroyed");
            foreach (GameObject obj in orderSprites)
            {
                Destroy(obj);
            }
            itemPick.table2Placed = false;
        }

        if (isTable3Placed && player.velocity.magnitude < 0.1f || itemPick.dishInT3 != null)
        {
            orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite3");
            Debug.Log("Destroyed");
            foreach (GameObject obj in orderSprites)
            {
                Destroy(obj);
            }
            itemPick.table3Placed = false;
        }
    }

    public void ResetTable(string table)
    {
        if (table == "1"){
            orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite1");
            Debug.Log("Destroyed");
            foreach (GameObject obj in orderSprites)
            {
            Destroy(obj);
            } 
            order1Spawned = false;
            cusMove.t1_occupied = false;
            timer1 = 0f;
            isTimer1Active = false;
        } else if (table == "2")
        {
            orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite2");
            Debug.Log("Destroyed");
            foreach (GameObject obj in orderSprites)
            {
                Destroy(obj);
            }
            order2Spawned = false;
            cusMove.t2_occupied = false;
            timer2 = 0f;
            isTimer2Active = false;
        } else if (table == "3")
        {
            orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite3");
            Debug.Log("Destroyed");
            foreach (GameObject obj in orderSprites)
            {
                Destroy(obj);
            }
            order3Spawned = false;
            cusMove.t3_occupied = false;
            timer3 = 0f;
            isTimer3Active = false;
        }

    }
}
