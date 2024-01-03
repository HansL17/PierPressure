using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SpawnCust : MonoBehaviour
{
    //Variables
    public GameObject[] LowModels; //What will spawn
    public GameObject[] HighModels;
    private GameObject ObjectToSpawn;
    public GameObject spawnedObject;
    float time;
    public float timeDelay;
    public float cusCount;
    public float totalCus;
    public Transform spawnPosition; // Position where the object should be spawned

    //Script reference
    public CustomerLine customerLine;
    public AgentType agenttype;
    public SoundScript SFX;

    private int custTier;
    private Scene currentScene;

 
    // Start is called before the first frame update
    void Start()
    {
        time = 0f; //Starting time
        timeDelay = 5f; //Delay in seconds
        cusCount = 0;
        totalCus = 0;

        SFX = GameObject.Find("SoundDesign").GetComponent<SoundScript>();
        currentScene = SceneManager.GetActiveScene();
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
        if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5")
        {
            //Randomize between hightier and lowtier customers
            int r = UnityEngine.Random.Range(1,11);
            if (r < 7) {
                custTier = 1;
            } else {
                custTier = 2;
            }
        } else {custTier = 1;}

        if (custTier == 1){
        //Randomize between male and female customer
            int r = UnityEngine.Random.Range(1,3);
            if (r == 1) {
                ObjectToSpawn = LowModels[0];
            } else {
                ObjectToSpawn = LowModels[1];
            }
            Debug.Log(r);

            //Modify the transform settings of the model
            Quaternion newRotation = Quaternion.Euler(0f, 180f, 0f);
            Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);

            //Instantiate the object with the modified transform settings and NavMeshAgent
            spawnedObject = Instantiate(ObjectToSpawn, spawnPosition.position, newRotation);
            spawnedObject.transform.localScale = newScale;
            spawnedObject.tag = "Customer";
            spawnedObject.layer = 8;

            //Add a NavMeshAgent to the spawnwed customer
            // NavMeshAgent customer = spawnedObject.AddComponent<NavMeshAgent>();
            // customer.radius = 0.05f;
            // customer.height = 0.15f;
            // customer.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
            // customer.agentTypeID = NavMesh.GetSettingsByIndex(1).agentTypeID;
            // customer.baseOffset = 0f;

            //Add a Box Collider to the spawned customer
            BoxCollider boxCollider = spawnedObject.AddComponent<BoxCollider>();
            float width = 0.7416129f;
            float height = 1.930175f;
            float depth = 0.5f;
            boxCollider.center = new Vector3(0, 1, 0);
            boxCollider.size = new Vector3(width, height, depth);
        }

        if (custTier == 2){
        //Randomize between male and female customer
            int r = UnityEngine.Random.Range(1,3);
            if (r == 1) {
                ObjectToSpawn = HighModels[0];
            } else {
                ObjectToSpawn = HighModels[1];
            }
            Debug.Log(r);

            //Modify the transform settings of the model
            Quaternion newRotation = Quaternion.Euler(0f, 180f, 0f);
            Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);

            //Instantiate the object with the modified transform settings and NavMeshAgent
            spawnedObject = Instantiate(ObjectToSpawn, spawnPosition.position, newRotation);
            spawnedObject.transform.localScale = newScale;
            spawnedObject.tag = "HighCustomer";
            spawnedObject.layer = 8;

            //Add a NavMeshAgent to the spawnwed customer
            // NavMeshAgent customer = spawnedObject.AddComponent<NavMeshAgent>();
            // customer.radius = 0.05f;
            // customer.height = 0.15f;
            // customer.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
            // customer.agentTypeID = NavMesh.GetSettingsByIndex(1).agentTypeID;
            // customer.baseOffset = 0f;

            //Add a Box Collider to the spawned customer
            BoxCollider boxCollider = spawnedObject.AddComponent<BoxCollider>();
            float width = 0.7416129f;
            float height = 1.930175f;
            float depth = 0.5f;
            boxCollider.center = new Vector3(0, 1, 0);
            boxCollider.size = new Vector3(width, height, depth);
        }
        //Add the object to the customer lineup
        SFX.CustomerSound();
        customerLine.AddToLineup(spawnedObject.transform);
        print("Customer spawned"); //Log
        cusCount++;
    }
}
