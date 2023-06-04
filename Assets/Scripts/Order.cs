using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public CustomerMove cusMove; //Reference to customer move script
    public Sprite order;
    private bool isTimerActive = false;
    private bool order1Spawned = false;
    private bool order2Spawned = false;
    private float timerDuration = 10f; // Duration in seconds
    private float timer;
    // Update is called once per frame

    void Start()
    {
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); //Get script
    }

    void Update()
    {
        if (cusMove.t1_occupied)
        {
            // Start the timer
            if (!isTimerActive)
            {
                timer = timerDuration;
                isTimerActive = true;
            }

            // Update the timer
            timer -= Time.deltaTime;

            // Check if the timer has completed
            if (timer <= 0f && order1Spawned == false)
            {
                GameObject orderSprite = new GameObject("OrderSprite");
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3 (24.98f, 1.316f, 5.23f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);

                timer = 0f;
                isTimerActive = false;
                order1Spawned = true;
            }
        }
        else if (cusMove.t2_occupied)
        {
            // Start the timer
            if (!isTimerActive)
            {
                timer = timerDuration;
                isTimerActive = true;
            }

            // Update the timer
            timer -= Time.deltaTime;

            // Check if the timer has completed
            if (timer <= 0f && order2Spawned == false)
            {
                GameObject orderSprite = new GameObject("OrderSprite");
                SpriteRenderer spriteRenderer = orderSprite.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = order;
                orderSprite.transform.position = new Vector3 (29.892f, 1.316f, 5.23f);
                orderSprite.transform.localScale = new Vector3 (0.10728f, 0.10728f, 0.10728f);

                timer = 0f;
                isTimerActive = false;
                order2Spawned = true;
            }
        }
            else
        {
            // Reset the timer and boolean if the boolean variable is no longer active
            timer = 0f;
            isTimerActive = false;
        }
    }
}
