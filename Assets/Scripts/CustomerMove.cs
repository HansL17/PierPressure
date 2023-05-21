using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMove : MonoBehaviour
{

    private GameObject selectedObject; //Current selected object
    public bool isHighlighted = false; //Flag to detect highlighted object

    public float speed = 5f; //Speed of the object

    private string cusTag = "Customer"; //Setting what object is labelled customer
    private string groundTag = "Ground"; //Setting what object is labelled ground


    private NavMeshAgent agent; //Navmesh of object
    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If left mouse button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (isHighlighted)
                {
                    if(clickedObject != selectedObject)
                    {
                        if (hit.collider.CompareTag(groundTag))
                        {
                            
                            MoveObject(clickedObject);
                        }
                    }

                    DisableHighlight();
                }
                else
                {
                    EnableHighlight(clickedObject);
                }
            }
        }
    }

    private void EnableHighlight(GameObject obj)
    {
        selectedObject = obj;

        if (selectedObject.CompareTag(cusTag))
        {
            isHighlighted = true;

            //Highlight
            Renderer renderer = selectedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow;
            }
        }
    }

    private void DisableHighlight()
    {
        isHighlighted = false;

        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.blue;
        }

        selectedObject = null;
    }

    private void MoveObject(GameObject destination)
    {
        //Log the movement
        Debug.Log("Moving" + selectedObject.name + " to " + destination.name);

        //Reset the color of the object
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.material.color = Color.blue;
        }

        //Move selected object to destination
        agent.SetDestination(hit.point);
    }
}
