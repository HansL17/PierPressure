using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class OrderTable3 : MonoBehaviour
{
    public CustomerMove cusMove; //Reference to customer move script
    public ItemPickup itemPick; // Reference to item pick up script
    [SerializeField] NavMeshAgent player; // Reference to Player NavMeshAgent

    public Sprite[] Orders;

    private bool isTimerActive = false;
    public bool order3Spawned = false;
    public float timer3Duration = 5f;
    public float timer3;

    public GameObject dishPos3;

    GameObject orderSprite;
    GameObject[] orderSprites;
    
    public bool isTable3Placed;
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
        isTable3Placed = itemPick.table3Placed;

            if (cusMove.t3_occupied && order3Spawned == false)
            {
                // Start the timer
                if (!isTimerActive)
                {
                    timer3 = timer3Duration;
                    isTimerActive = true;
                }

                // Update the timer
                timer3 -= Time.deltaTime;

                // Check if the timer has completed
                if (timer3 <= 0f && order3Spawned == false)
                {
                    order3Spawned = true;
                    orderSprite = new GameObject("OrderSprite3");
                    orderSprite.tag = "OrderSprite3";
                    SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                    
                    if(cusMove.customerInT3.CompareTag(cusMove.cusTag)){
                        int r = UnityEngine.Random.Range(1,11);
                        if (r < 9) {
                            spriteRenderer.sprite = Orders[0];
                            OrderNum = 1;}
                        else {
                            spriteRenderer.sprite = Orders[1];
                            OrderNum = 2;}
                    } else if (cusMove.customerInT3.CompareTag(cusMove.HcusTag)){
                        int r = UnityEngine.Random.Range(1,11);
                        if (r < 9) {
                            spriteRenderer.sprite = Orders[1];
                            OrderNum = 2;}
                        else {
                            spriteRenderer.sprite = Orders[0];
                            OrderNum = 1;}
                    }

                    orderSprite.transform.position = new Vector3 (dishPos3.transform.position.x, dishPos3.transform.position.y + 0.7f, dishPos3.transform.position.z);
                    orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                    timer3 = 0f;
                    isTimerActive = false;
                } 
            }
        
        if (isTable3Placed && player.velocity.magnitude < 0.1f)
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

    public void ResetTable()
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
        isTimerActive = false;
    }
}
