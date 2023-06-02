using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 targetPos = targetPosition.position + new Vector3(0f, 0f, index * spacing);
            StartCoroutine(MoveObject(obj, targetPos));
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

