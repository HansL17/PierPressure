using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSit : MonoBehaviour
{
    public GameObject chair;             // Reference to the chair GameObject
    public GameObject objectToIgnore;    // Reference to the GameObject to ignore

    private bool isSitting = false;

    public CustomerMove cusMove;

    void Start()
    {
        chair = GameObject.Find("wooden_chair");
        objectToIgnore = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject collides with the chair's collider and is not the object to ignore
        if (other.gameObject == chair && other.gameObject != objectToIgnore && !isSitting)
        {
            // Position the GameObject on the chair
            transform.position = chair.transform.position;
            transform.rotation = chair.transform.rotation;


            // Parent the GameObject to the chair
            transform.SetParent(chair.transform);

            // Disable Rigidbody to stop physics simulation
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            isSitting = true;
        }
    }
}
