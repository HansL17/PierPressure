using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClick : MonoBehaviour
{
    public float movementSpeed;
    public GameObject player;
    public GameObject[] waypoint;
    int current = 0;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving && player != null)
        {
            // Calculate the direction from the player to this object
            Vector3 direction = (new Vector3(waypoint[current].transform.position.x, player.transform.position.y, waypoint[current].transform.position.z) - player.transform.position).normalized;

            // Check if this object has reached close enough to the targetObject
            float distance1 = Vector3.Distance(player.transform.position, waypoint[0].transform.position);
            float distance2 = Vector3.Distance(player.transform.position, waypoint[1].transform.position);
            float distance3 = Vector3.Distance(player.transform.position, waypoint[2].transform.position);

            if (distance1 > 1f && distance1 <= distance2 && distance1 <= distance3)
            {
                // Move the player towards this object based on the direction and movement speed
                player.transform.position += direction * movementSpeed * Time.deltaTime;
            } else if (distance2 > 1f && distance2 < distance1 && distance2 < distance3)
            {
                current++;
                // Move the player towards this object based on the direction and movement speed
                player.transform.position += direction * movementSpeed * Time.deltaTime;
                Invoke("resetCurrent", 0.5f);
            } else if (distance3 > 1f && distance3 < distance1 && distance3 < distance2)
            {
                current++;
                current++;
                // Move the player towards this object based on the direction and movement speed
                player.transform.position += direction * movementSpeed * Time.deltaTime;
                resetCurrent();
            } else {
                // Stop moving
                isMoving = false;
            }
        }
    }

    private void OnMouseDown()
    {
        // Start moving towards the target object
        isMoving = true;
    }

    private void resetCurrent()
    {
        current = 0;
    }
}

