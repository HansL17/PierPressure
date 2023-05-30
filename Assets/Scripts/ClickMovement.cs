using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour
{

    public Camera camera;
    //public CustomerMove custMove;

    private RaycastHit hit;

    private NavMeshAgent agent; //Navmesh of object


    private string tableTag = "Ground"; //Setting what object is labelled ground

    public bool isHoldingDown; //If mousebutton is held down or pressed
    public int tapTimes; //How many times mousebutton has been pressed
    public float resetTimer;
    


    IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
    }



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // && custMove.isHighlighted == false
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(tableTag)) // && custMove.isHighlighted == false
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("ResetTapTimes");
            tapTimes++;
            agent.speed = 3;
            //SingleClick
        }

        if (tapTimes >= 2)
        {
            tapTimes = 0;
            agent.speed = 6;
            //DoubleClick
        }

        if (Input.GetMouseButton(0))
        {
            isHoldingDown = true;   
        }
        else
            isHoldingDown = false;
        
    }
}
