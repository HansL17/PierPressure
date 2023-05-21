using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCust : MonoBehaviour
{
    //Variables
    public Object ObjectToSpawn; //What will spawn
    float time;
    float timeDelay;
    float cusCount;
 
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
        if(time >= timeDelay && cusCount == 0) // If Time is equal to time delay
        {
            Spawn();
            time = 0f;
        }

    }

    private void Spawn()
    {
        Instantiate(ObjectToSpawn, transform.position, transform.rotation);
        print("Customer spawned"); //Log
        cusCount = 1;
    }
}
