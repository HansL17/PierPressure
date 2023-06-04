using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSit : MonoBehaviour
{
    public GameObject chair; //Where to put the customer

    public bool isSitting = false;

    public CustomerMove cusMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            Debug.Log("Triggered");
            // Position the GameObject on the chair
            transform.position = chair.transform.position;
            transform.rotation = chair.transform.rotation;

            isSitting = true;
        }
    }
}
