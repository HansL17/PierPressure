using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CustomerMove : MonoBehaviour
{

    private GameObject selectedObject; //Current selected object
    
    public GameObject customerInT1;
    public GameObject customerInT2;
    public GameObject customerTable;
    public Canvas cusBar;
    public RectTransform cusBarRect;


    public bool isHighlighted = false; //Flag to detect highlighted object

    public bool t1_occupied = false;
    public bool t2_occupied = false;

    public float speed = 5f; //Speed of the object

    private string cusTag = "Customer"; //Setting what object is labelled customer
    //private string tableTag = "Table"; //Setting what object is labelled table

    //Script references
    public CustomerLine cusLine;
    public Scoring scores;
    public PatienceBar pBar;
    public Lvl2Upgrade lvl2UG = null;


    private NavMeshAgent agent; //Navmesh of object
    private RaycastHit hit;

    void Awake()
    {
        cusLine = GameObject.Find("CustomerSpawn").GetComponent<CustomerLine>(); //Get script
        scores = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get script
        pBar = GameObject.Find("CustomerLine").GetComponent<PatienceBar>(); // Get script

        


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
                            Action1Done(); //Action 1 is done

                            //Redefining customer
                            customerInT1 = selectedObject;

                            //table 1 is occupied
                            t1_occupied = true;
                        }
                        else if (hit.collider.gameObject.name == "T2_chair" && t2_occupied == false)

                        {
                            Transform wp2 = GameObject.Find("customerWP2").GetComponent<Transform>();

                            cusLine.RemoveFromLineup(selectedObject.transform);
                            //Log the movement
                            Debug.Log("Moving" + selectedObject.name);

                            MoveObject(wp2.transform);
                            Action1Done(); //Action 1 is done

                            //Redefining customer
                            customerInT2 = selectedObject;

                            //table 2 is occupied
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
            scores.AddScore();
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
        // Rotate the bar to face the camera
        cusBar = agent.GetComponentInChildren<Canvas>();
        cusBarRect = cusBar.GetComponent<RectTransform>();
        cusBarRect.rotation = Quaternion.Euler(0f, 0f, -180f);
        cusBarRect.pivot = new Vector2(0.3f, 0.5f);
    }

    private void Action1Done()
    {
        scores.isAction1Done = true;
        scores.isAction2Done = false;
        scores.consecutiveActions1++;
        scores.consecutiveActions2 = 0;
        Debug.Log("Action 1 Done");
    }

}