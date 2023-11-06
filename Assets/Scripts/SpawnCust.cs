using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnCust : MonoBehaviour
{
    //Variables
    public GameObject ObjectToSpawn; //What will spawn
    float time;
    public float timeDelay;
    public float cusCount;
    public float totalCus;
    public Transform spawnPosition; // Position where the object should be spawned

    //Script reference
    public CustomerLine customerLine;
    public AgentType agenttype;

 
    // Start is called before the first frame update
    void Start()
    {
        time = 0f; //Starting time
        timeDelay = 5f; //Delay in seconds
        cusCount = 0;
        totalCus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if(time >= timeDelay && cusCount < 3 && totalCus != 6) // If Time is equal to time delay
        {
            Spawn();
            totalCus++;
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
        spawnedObject.tag = "Customer";
        spawnedObject.layer = 8;

        //Add a NavMeshAgent to the spawnwed customer
        NavMeshAgent customer = spawnedObject.AddComponent<NavMeshAgent>();
        customer.radius = 0.05f;
        customer.height = 0.15f;
        customer.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        customer.agentTypeID = NavMesh.GetSettingsByIndex(1).agentTypeID;
        customer.baseOffset = 0.075f;

        //Add a Box Collider to the spawned customer
        BoxCollider boxCollider = spawnedObject.AddComponent<BoxCollider>();
        float width = 0.04034754f;
        float height = 0.1559255f;
        float depth = 0.0263764f;
        boxCollider.size = new Vector3(width, height, depth); 

        //Make customer blue
        MeshRenderer[] meshRenderers = spawnedObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            Color blue = Color.blue;
            meshRenderer.material.color = blue;
        }


        //Add the object to the customer lineup
        customerLine.AddToLineup(spawnedObject.transform);
        print("Customer spawned"); //Log
        cusCount++;
    }
}
