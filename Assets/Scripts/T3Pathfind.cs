using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class T3Pathfind : MonoBehaviour
{
    public Transform table;
    public NavMeshAgent Player;
    public GameObject waypoint1;
    public GameObject waypoint2;
    public bool fromA = false;
    public bool fromB = false;
    public T2Pathfind t2pf;

    private void Start()
    {
        t2pf = GameObject.Find("T2_table").GetComponent<T2Pathfind>();
    }

    private void OnMouseDown()
    {
        if (Player != null)
        {
            t2pf.OnStart = false;
            NavigateToClosestWaypoint();
        }
    }

    void NavigateToClosestWaypoint()
    {
        if (waypoint1 != null && waypoint2 != null)
        {
            // Calculate distances to waypoints
            float distanceToA = Vector3.Distance(Player.transform.position, waypoint1.transform.position);
            float distanceToB = Vector3.Distance(Player.transform.position, waypoint2.transform.position);

            // Navigate to the closest waypoint
            if (distanceToA <= distanceToB)
            {
                fromA = true;
                Player.SetDestination(waypoint1.transform.position);
            }
            else
            {
                fromB = true;
                Player.SetDestination(waypoint2.transform.position);
            }
        }
        else
        {
            Debug.LogError("Waypoints are not assigned.");
        }
    }
}
