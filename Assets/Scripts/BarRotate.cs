using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRotate : MonoBehaviour
{

    public Canvas pBar;
    public CustomerMove cusMove;
    public bool Moving;
    public bool Sittingdown;

    // Start is called before the first frame update
    void Start()
    {
        pBar = GetComponentInChildren<Canvas>();
        Sittingdown = false;
        Moving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Moving == true)
        {
            
            // Get the current rotation of the Canvas
            Quaternion currentRotation = pBar.transform.rotation;

            // Calculate the new rotation by adding 90 degrees in the Y axis
            Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y - 90f, currentRotation.eulerAngles.z);

            // Apply the new rotation to the Canvas
            pBar.transform.rotation = newRotation;

            Sittingdown = true;
            Moving = false;
        }

    }
}
