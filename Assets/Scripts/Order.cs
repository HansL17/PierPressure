using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public CustomerMove cusMove; //Reference to customer move script
    public Sprite order;
    private bool isTimer1Active = false;
    private bool isTimer2Active = false;
    private bool order1Spawned = false;
    private bool order2Spawned = false;
    private float timer1Duration = 7f; // Duration in seconds
    private float timer2Duration = 9f;
    public float timer1;
    public float timer2;
    // Update is called once per frame

    void Start()
    {
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); //Get script
    }

    void Update()
    {
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
                GameObject orderSprite = new GameObject("OrderSprite1");
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3 (25.137f, 1.316f, 2.831f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);
                timer1 = 0f;
                isTimer1Active = false;
            }
        }
        else if (cusMove.t2_occupied && order2Spawned == false)
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
                GameObject orderSprite = new GameObject("OrderSprite2");
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3 (29.295f, 1.316f, 2.831f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);

                timer2 = 0f;
                isTimer2Active = false;
                order2Spawned = true;
            }
        }
    }
}
