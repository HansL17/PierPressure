using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class T1Pathfind : MonoBehaviour
{
    public NavMeshAgent Player;
    public Transform[] waypoints;
    public int currentWaypointIndex = 0;
    public T3Pathfind t3pf;
    public ItemPickup itemPick;
    public bool go = false;
    public bool OnTable = false;
    private Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level3"){
            t3pf = GameObject.Find("T3_table").GetComponent<T3Pathfind>();
        }
        itemPick = GameObject.Find("Player").GetComponent<ItemPickup>();
    }

    private void OnMouseDown()
    {
        OnTable = true;
        if (currentScene.name == "Level3"){
            if (t3pf.fromA == true)
            {
                MoveToNextWaypoint();
                go = true;
                t3pf.fromA = false;
            }
            else{
            Player.SetDestination(waypoints[1].transform.position);
            }
            if (itemPick.OnWaypoint == true)
            {
                MoveToNextWaypoint();
                go = true;
                itemPick.OnWaypoint = false;
            }
            if (itemPick.OnTrash == true)
            {
                MoveToNextWaypoint();
                go = true;
                itemPick.OnTrash = false;
            }
        } else {Player.SetDestination(waypoints[0].transform.position);}
        
    }

    private void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Player.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex++;
        }
        else
        {
            go = false;
            currentWaypointIndex = 0;
        }
    }

    private void Update()
    {
        if(go == true){
            if (!Player.pathPending && Player.remainingDistance <= Player.stoppingDistance)
            {
                if (!Player.hasPath || Player.velocity.sqrMagnitude == 0f)
                {
                StartCoroutine(WaitAndMoveToNextWaypoint());
                }
            }
        }   
    }

    private System.Collections.IEnumerator WaitAndMoveToNextWaypoint()
    {
        yield return new WaitForSeconds(0f);
        MoveToNextWaypoint();
    }
}
