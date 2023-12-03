using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class T1Pathfind : MonoBehaviour
{
    public NavMeshAgent Player;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public T3Pathfind t3pf;
    public bool go = false;
    private Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level3"){
            t3pf = GameObject.Find("T3_table").GetComponent<T3Pathfind>();
        }
    }

    private void OnMouseDown()
    {
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
