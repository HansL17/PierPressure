using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfindingTrigger : MonoBehaviour
{
    public Transform table;
    public NavMeshAgent Player;
    public GameObject waypoint;
    public float movementSpeed = 3f;

    private void Start()
    {
        Player.speed = movementSpeed;
    }

    private void OnMouseDown()
    {
        if (Player != null)
        {
            Player.SetDestination(waypoint.transform.position);
        }
    }
}
