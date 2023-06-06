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
    private bool order1Spawned = false;
    private bool order2Spawned = false;
    private float timer1Duration = 7f;
    private float timer2Duration = 9f;
    public float timer1;
    public float timer2;

    GameObject orderSprite;
    GameObject[] orderSprites;
    
    public bool isTable1Placed;
    public bool isTable2Placed;

    void Start()
    {
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); //Get script
        itemPick = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get script
    } 

    void Update()
    {
               
        isTable1Placed = itemPick.table1Placed;
        isTable2Placed = itemPick.table2Placed;

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
                orderSprite.transform.position = new Vector3 (25.137f, 1.316f, 2.831f);
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
                orderSprite.transform.position = new Vector3 (29.295f, 1.316f, 2.831f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                timer2 = 0f;
                isTimer2Active = false;
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
    }
}
