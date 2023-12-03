using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class OrderTable1 : MonoBehaviour
{
    public CustomerMove cusMove; //Reference to customer move script
    public ItemPickup itemPick; // Reference to item pick up script
    [SerializeField] NavMeshAgent player; // Reference to Player NavMeshAgent

    public Sprite[] Orders;

    private bool isTimerActive = false;
    public bool order1Spawned = false;
    public float timer1Duration = 5f;
    public float timer1;

    public GameObject dishPos1;

    GameObject orderSprite;
    GameObject[] orderSprites;
    
    public bool isTable1Placed;
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
        isTable1Placed = itemPick.table1Placed;

            if (cusMove.t1_occupied && order1Spawned == false)
            {
                // Start the timer
                if (!isTimerActive)
                {
                    timer1 = timer1Duration;
                    isTimerActive = true;
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
                    if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5")
                    {
                        if(cusMove.customerInT1.CompareTag(cusMove.cusTag)){
                            int r = UnityEngine.Random.Range(1,11);
                            if (r < 9) {
                                spriteRenderer.sprite = Orders[0];
                                OrderNum = 1;}
                            else {
                                spriteRenderer.sprite = Orders[1];
                                OrderNum = 2;}
                        } else if (cusMove.customerInT1.CompareTag(cusMove.HcusTag)){
                            int r = UnityEngine.Random.Range(1,11);
                            if (r < 9) {
                                spriteRenderer.sprite = Orders[1];
                                OrderNum = 2;}
                            else {
                                spriteRenderer.sprite = Orders[0];
                                OrderNum = 1;}
                        }
                    } else {
                        spriteRenderer.sprite = Orders[0];
                        OrderNum = 1;
                    }                       

                    orderSprite.transform.position = new Vector3 (dishPos1.transform.position.x, dishPos1.transform.position.y + 0.7f, dishPos1.transform.position.z);
                    orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                    timer1 = 0f;
                    isTimerActive = false;
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
    }

    public void ResetTable()
    {
        orderSprites = GameObject.FindGameObjectsWithTag("OrderSprite1");
        Debug.Log("Destroyed");
        foreach (GameObject obj in orderSprites)
        {
        Destroy(obj);
        } 
        order1Spawned = false;
        cusMove.t1_occupied = false;
        timer1 = 0f;
        isTimerActive = false;
    }
}
