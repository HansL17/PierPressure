using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TableBar : MonoBehaviour
{
    [SerializeField] private Slider currentSlider;
    public Canvas t1Bar;
    public Canvas t2Bar;
    public Slider tab1Slider;
    public Slider tab2Slider;
    public float maxEat = 10f; //max patience (in seconds)
    public float currentTab; //current patience (in seconds)
    private NavMeshAgent agent;

    private ItemPickup itPick;
    public CustomerMove cusMove;
    public SpawnCust spawnCus;

    void Awake()
    {
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
    }
    void Start()
    {
        currentTab = 10f;
        tab1Slider.value = currentTab;
        tab2Slider.value = currentTab;
    }


    void Update()
    {
        if (itPick.table1Placed)
        {
            t1Bar.gameObject.SetActive(true);
            StartCoroutine(DepleteTableBar(t1Bar));
        }

        if (itPick.table2Placed)
        {
            t2Bar.gameObject.SetActive(true);
            StartCoroutine(DepleteTableBar(t2Bar));
        } 
    }

    public IEnumerator DepleteTableBar(Canvas bar)
    {
        while (currentTab > 0f)
        {
            Slider slider = bar.GetComponentInChildren<Slider>();
            currentTab -= Time.deltaTime;
            slider.value -= Time.deltaTime;
            yield return null;
        }
        

        // //When Patience bar is done, perform any necessary action with TabGone()
        StartCoroutine(TabGone(bar));
    }

    private IEnumerator TabGone(Canvas bar)
    {
        Debug.Log("Customer is done eating");
        bar.gameObject.SetActive(false);
        Slider slider = bar.GetComponentInChildren<Slider>();
        slider.value = maxEat;

        if(bar.gameObject.name == "Table1Bar")
        {
            agent = cusMove.customerInT1.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }
            Destroy(cusMove.customerInT1);
            spawnCus.cusCount--;
            cusMove.t1_occupied = false;
            Destroy(itPick.dishInT1);
            itPick.table1Placed = false;
        }
        if(bar.gameObject.name == "Table2Bar")
        {
            agent = cusMove.customerInT2.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }
            Destroy(cusMove.customerInT2);
            spawnCus.cusCount--;
            cusMove.t2_occupied = false;
            Destroy(itPick.dishInT2);
            itPick.table2Placed = false;
        }
    }

}
