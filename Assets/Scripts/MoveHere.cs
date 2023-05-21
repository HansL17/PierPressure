using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHere : MonoBehaviour
{

    public Transform Table1; //Location Target 1
    public Transform Table2; //Location Target 2
    public Transform Table3; //Location Target 3

    public float speed = 5; //Customer Speed

    private Transform CurrentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            CurrentTarget = Table1;
            print("Table 1 was pressed");
        }

        if (Input.GetKeyDown("2"))
        {
            CurrentTarget = Table2;
            print("Table 2 was pressed");
        }

        if (Input.GetKeyDown("3"))
        {
            CurrentTarget = Table3;
            print("Table 3 was pressed");
        }

        if (CurrentTarget != null) transform.position = Vector3.MoveTowards(transform.position, CurrentTarget.position, speed * Time.deltaTime);
    }
}
