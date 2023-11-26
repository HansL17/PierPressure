using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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
    public Order order;


    private void Awake()
    {
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusLine = GameObject.Find("CustomerSpawn").GetComponent<CustomerLine>(); //Get script
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
        order = GameObject.Find("DishPosition").GetComponent<Order>(); //Get script
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPatience = 40;
        slider.value = currentPatience;
        StartCoroutine(DepletePatienceBar());
        //Start the patience countdown
        agent = _cus.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(itPick.table1Placed)
        {
            Canvas toDelete = cusMove.customerInT1.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
        }
        if(itPick.table2Placed)
        {
            Canvas toDelete = cusMove.customerInT2.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
        }
        if(itPick.table3Placed)
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
        Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
        if (agent.gameObject == cusMove.customerInT1)
        {
            Canvas toDelete = cusMove.customerInT1.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
            order.ResetTable("1");
        }
        if (agent.gameObject == cusMove.customerInT2)
        {
            Canvas toDelete = cusMove.customerInT2.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
            order.ResetTable("2");
        }
        if (agent.gameObject == cusMove.customerInT3)
        {
            Canvas toDelete = cusMove.customerInT3.GetComponentInChildren<Canvas>();
            Destroy(toDelete);
            order.ResetTable("3");
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
