using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMove : MonoBehaviour
{

    private GameObject selectedObject; //Current selected object
    public bool isHighlighted = false; //Flag to detect highlighted object
    public bool t1_occupied = false;
    public bool t2_occupied = false;

    public GameObject[] waypoints; //Destination of the selected object

    public float speed = 5f; //Speed of the object

    private string cusTag = "Customer"; //Setting what object is labelled customer
    private string tableTag = "Table"; //Setting what object is labelled table

    //Script references
    public CustomerLine cusLine;
    public Scoring scores;
    
   


    private NavMeshAgent agent; //Navmesh of object
    private RaycastHit hit;

    void Awake()
    {
        cusLine = GameObject.Find("CustomerSpawn").GetComponent<CustomerLine>(); //Get script
        scores = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get script  
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If left mouse button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (isHighlighted == true)
                {
                    if (clickedObject != selectedObject)
                    {
                        if (hit.collider.gameObject.name == "T1_chair" && t1_occupied == false)
                        {
                            Transform wp1 = GameObject.Find("customerWP1").GetComponent<Transform>();

                            cusLine.RemoveFromLineup(selectedObject.transform);
                            //Log the movement
                            Debug.Log("Moving" + selectedObject.name);
                            MoveObject(wp1.transform);
                            t1_occupied = true;
                        }
                        else if (hit.collider.gameObject.name == "T2_chair" && t2_occupied == false)

                        {
                            Transform wp2 = GameObject.Find("customerWP2").GetComponent<Transform>();

                            cusLine.RemoveFromLineup(selectedObject.transform);
                            //Log the movement
                            Debug.Log("Moving" + selectedObject.name);
                            MoveObject(wp2.transform);
                            t2_occupied = true;
                        }  

                        DisableHighlight();
                    }


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
            MeshRenderer[] meshRenderers = selectedObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                Color yellow = Color.yellow;
                meshRenderer.material.color = yellow;
            }
        }
    }

    private void DisableHighlight()
    {
        isHighlighted = false;

        MeshRenderer[] meshRenderers = selectedObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            Color blue = Color.blue;
            meshRenderer.material.color = blue;
        }

        selectedObject = null;
    }

    public void MoveObject(Transform destination)
    {
        Vector3 targetPosition = destination.transform.position;
        scores.AddScore(10);
        agent = selectedObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPosition);
        StartCoroutine(RotateAgent(targetPosition));
    }

    private IEnumerator RotateAgent(Vector3 targetPosition)
    {
        // Wait until the object reaches its destination
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        // Rotate the agent towards the opposite direction
        agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, Quaternion.Euler(0f, 90f, 0f), 90f);
    }
}