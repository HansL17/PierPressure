using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Queue<Transform> clickQueue = new Queue<Transform>();
    public bool isMoving = false;

    CustomActions input;
    NavMeshAgent agent;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    public Transform[] waypoints;
    public Transform trashWP;
    public GameObject DishLoc1;
    public GameObject DishLoc2;
    public ItemPickup itemPick;

    float lookRotationSpeed = 8f;

    void Awake()
    {
        itemPick = GameObject.Find("Player").GetComponent<ItemPickup>();
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
            if(hit.transform.CompareTag("Table") || (hit.transform.CompareTag("Trash")) || (hit.transform.CompareTag("Dish")))
            {
                clickQueue.Enqueue(hit.transform);
                Debug.Log(clickQueue);
                if (!isMoving)
                {
                    StartCoroutine(PerformNextAction());
                }
            }
    }
    }
    Transform FindNearestWaypoint(Transform position)
    {
        Transform nearest = null;
        float minDistance = float.MaxValue;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(position.position, waypoint.position);
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

    private IEnumerator PerformNextAction()
    {
        if (clickQueue.Count == 0)
        {
            yield break;
        }

        isMoving = true;
        Transform target = clickQueue.Dequeue();
        Debug.Log(clickQueue);

        if (target.CompareTag("Table"))
        {
            Transform nearestWaypoint = FindNearestWaypoint(target);
                if (nearestWaypoint != null)
                {
                    while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                    {
                        yield return null;
                    }
                    agent.SetDestination(nearestWaypoint.position);
                        
                    if (clickEffect != null)
                    {
                        Instantiate(clickEffect, nearestWaypoint.position + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                    }
                }
        }
        else if (target.CompareTag("Trash"))
        {
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }
            agent.SetDestination(trashWP.transform.position);
            StartCoroutine(itemPick.ThrowingDish());
        }
        else if (target.CompareTag("Dish"))
        {
            // Pick up the item if it's not already held
            if (itemPick.heldItem == null)
            {
                itemPick.heldItem = target.gameObject;
                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                    {
                        yield return null;
                    }
                if (target.gameObject.name == "dish1(Clone)"){
                    agent.SetDestination(DishLoc1.transform.position);
                } else if (target.gameObject.name == "dish2(Clone)"){
                    agent.SetDestination(DishLoc2.transform.position);
                }
                itemPick.isMovingToDestination = true;
                itemPick.OnWaypoint = true;
            }
        }
        isMoving = false;
        yield return new WaitForSeconds(0.1f); // Small delay before processing next action
        StartCoroutine(PerformNextAction());
    }
}
