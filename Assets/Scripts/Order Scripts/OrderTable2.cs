using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class OrderTable2 : MonoBehaviour
{
    public CustomerMove cusMove; //Reference to customer move script
    public ItemPickup itemPick; // Reference to item pick up script
    [SerializeField] NavMeshAgent player; // Reference to Player NavMeshAgent

    public Sprite[] Orders;

    private bool isTimerActive = false;
    public bool order2Spawned = false;
    public float timer2Duration = 5f;
    public float timer2;

    public GameObject dishPos2;

    GameObject orderSprite;
    GameObject[] orderSprites;
    
    public bool isTable2Placed;
    public int OrderNum = 0;
    
    private Scene currentScene;

    void Start()
    {
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); //Get script
        itemPick = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get script

        currentScene = SceneManager.GetActiveScene();        
    } 

    void Update()
    {
        isTable2Placed = itemPick.table2Placed;

            if (cusMove.t2_occupied && order2Spawned == false)
            {
                // Start the timer
                if (!isTimerActive)
                {
                    timer2 = timer2Duration;
                    isTimerActive = true;
                }

                // Update the timer
                timer2 -= Time.deltaTime;

                // Check if the timer has completed
                if (timer2 <= 0f && order2Spawned == false)
                {
                    order2Spawned = true;
                    orderSprite = new GameObject("OrderSprite2");
                    orderSprite.tag = "OrderSprite2";
                    SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                    if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5")
                    {
                        float randomValue = Random.value;
                        if (randomValue < 0.5f) {
                            spriteRenderer.sprite = Orders[0];
                            OrderNum = 1;}
                        else {
                            spriteRenderer.sprite = Orders[1];
                            OrderNum = 2;}
                    } else {
                        spriteRenderer.sprite = Orders[0];
                        OrderNum = 1;
                    }
                    orderSprite.transform.position = new Vector3 (dishPos2.transform.position.x, dishPos2.transform.position.y + 0.7f, dishPos2.transform.position.z);
                    orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                    timer2 = 0f;
                    isTimerActive = false;
                } 
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

    public void ResetTable()
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
        isTimerActive = false;
    }
}
