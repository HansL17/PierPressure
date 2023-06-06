using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerLine : MonoBehaviour
{
    public Transform targetPosition;  // Target position where objects should fall in line
    public float spacing = 1.0f;      // Spacing between objects in the lineup

    private Queue<Transform> objectQueue = new Queue<Transform>();

    public void AddToLineup(Transform obj)
    {
        objectQueue.Enqueue(obj);
        UpdateLineup();
    }

    public void RemoveFromLineup(Transform obj)
    {
        objectQueue.Dequeue();
        UpdateLineup();
        Debug.Log("Removing Object from Lineup");
    }

    public void UpdateLineup()
    {
        StartCoroutine(MoveObjects());
    }

    private IEnumerator MoveObjects()
    {
        int index = 0;

        foreach (Transform obj in objectQueue)
        {
            NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                Vector3 destination = targetPosition.position + new Vector3(0f, 0.30f, index * spacing);
                agent.SetDestination(destination);

                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }
                // Rotate the agent towards the opposite direction
                obj.rotation = Quaternion.RotateTowards(obj.rotation, Quaternion.Euler(0f, 180f, 0f), 180f);
            }

            index++;
            yield return new WaitForSeconds(1f);  // Delay between moving each object
        }
    }

    private IEnumerator MoveObject(Transform obj, Vector3 targetPos)
    {
        float speed = 5f;  // Speed at which the object moves

        while (Vector3.Distance(obj.position, targetPos) > 0.01f)
        {
            obj.position = Vector3.MoveTowards(obj.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}

