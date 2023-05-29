using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(time >= timeDelay && cusCount < 5) // If Time is equal to time delay
        {
            Spawn();
            time = 0f;
        }

    }

    private void Spawn()
    {
        GameObject spawnedObject = Instantiate(ObjectToSpawn, spawnPosition.position, Quaternion.identity);
        customerLine.AddToLineup(spawnedObject.transform);
        print("Customer spawned"); //Log
        cusCount++;
    }
}
