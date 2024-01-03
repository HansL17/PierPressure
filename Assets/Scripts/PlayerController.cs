using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // const string IDLE = "Idle";
    // const string WALK = "Walk";
    CustomActions input;

    NavMeshAgent agent;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    public Transform[] waypoints;

    float lookRotationSpeed = 8f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        input = new CustomActions();
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            Transform nearestWaypoint = FindNearestWaypoint(hit.point);
            if (nearestWaypoint != null)
            {
                agent.SetDestination(nearestWaypoint.position);
                
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, nearestWaypoint.position + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
    }
    }

    Transform FindNearestWaypoint(Vector3 position)
    {
        Transform nearest = null;
        float minDistance = float.MaxValue;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(position, waypoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = waypoint;
            }
        }

        return nearest;
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        //FaceTarget();
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 ,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }
}
