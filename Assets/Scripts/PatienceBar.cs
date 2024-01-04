using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatienceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject _cus;

    public float maxPatience = 40f; //max patience (in seconds)
    public float currentPatience; //current patience (in seconds)
    public NavMeshAgent agent;


    private ItemPickup itPick;
    public CustomerLine cusLine;
    public SpawnCust spawnCus;
    public CustomerMove cusMove;
    public OrderTable1 orderT1;
    public OrderTable2 orderT2;
    public OrderTable3 orderT3;
    public SoundScript BGM;
    public ScoreTally PDelete;


    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //Scripts
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusLine = GameObject.Find("CustomerSpawn").GetComponent<CustomerLine>(); //Get script
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
        orderT1 = GameObject.Find("DishPosition").GetComponent<OrderTable1>(); //Get script
        orderT2 = GameObject.Find("DishPosition2").GetComponent<OrderTable2>(); //Get script
        if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5")
        {orderT3 = GameObject.Find("DishPosition3").GetComponent<OrderTable3>();} else {orderT3 = null;}
        BGM = GameObject.Find("SoundDesign").GetComponent<SoundScript>();
        PDelete = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>(); // Get script

    }

    // Start is called before the first frame update
    void Start()
    {
        currentPatience = 50;
        slider.value = currentPatience;
        StartCoroutine(DepletePatienceBar());
        //Start the patience countdown
        agent = _cus.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(itPick.dishInT1 != null)
        {
            Canvas toDelete = cusMove.customerInT1.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
        }
        if(itPick.dishInT2 != null)
        {
            Canvas toDelete = cusMove.customerInT2.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
        }
        if(itPick.dishInT3 != null)
        {
            Canvas toDelete = cusMove.customerInT3.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
        }
    }

    private IEnumerator DepletePatienceBar()
    {
        while (currentPatience > 0f)
        {
            currentPatience -= Time.deltaTime;
            slider.value -= Time.deltaTime;
            yield return null;
        }

        //When Patience bar is done, perform any necessary action with PatienceGone()
        StartCoroutine(PatienceGone());
    }

    private IEnumerator PatienceGone()
    {
        Debug.Log("Patience Depleted");
        PDelete.NoPatience = true;

        Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
        if (agent.gameObject == cusMove.customerInT1)
        {
            Canvas toDelete = cusMove.customerInT1.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
            orderT1.ResetTable();
        }
        if (agent.gameObject == cusMove.customerInT2)
        {
            Canvas toDelete = cusMove.customerInT2.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
            orderT2.ResetTable();
        }
        if (agent.gameObject == cusMove.customerInT3)
        {
            Canvas toDelete = cusMove.customerInT3.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
            orderT3.ResetTable();
        }

        
        agent.SetDestination(exit.transform.position);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        Destroy(_cus);
        spawnCus.cusCount--;
    }
}
