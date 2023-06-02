using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnCust : MonoBehaviour
{
    //Variables
    public GameObject ObjectToSpawn; //What will spawn
    float time;
    float timeDelay;
    public float cusCount;
    public Transform spawnPosition; // Position where the object should be spawned

    //Script reference
    public CustomerLine customerLine;

 
    // Start is called before the first frame update
    void Start()
    {
        time = 0f; //Starting time
        timeDelay = 5f; //Delay in seconds
        cusCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if(time >= timeDelay && cusCount < 3) // If Time is equal to time delay
        {
            Spawn();
            time = 0f;
        }

    }

    private void Spawn()
    {
        //Modify the transform settings of the model
        Quaternion newRotation = Quaternion.Euler(0f, 180f, 0f);
        Vector3 newScale = new Vector3(7.270209f, 7.192486f, 7.270209f);

        //Instantiate the object with the modified transform settings and NavMeshAgent
        GameObject spawnedObject = Instantiate(ObjectToSpawn, spawnPosition.position, newRotation);
        spawnedObject.transform.localScale = newScale;
        NavMeshAgent customer = spawnedObject.AddComponent<NavMeshAgent>();
        customer.radius = 0.05f;
        customer.height = 0.2f;
        float baseOffset = 0.1f;
        customer.baseOffset = baseOffset;


        //Add the object to the customer lineup
        customerLine.AddToLineup(spawnedObject.transform);
        print("Customer spawned"); //Log
        cusCount++;
    }
}
