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
    public Canvas t3Bar;
    public Slider tab1Slider;
    public Slider tab2Slider;
    public Slider tab3Slider;
    public float maxEat = 5f; //max patience (in seconds)
    public float currentTab; //current patience (in seconds)
    private NavMeshAgent agent;

    private ItemPickup itPick;
    public CustomerMove cusMove;
    public SpawnCust spawnCus;
    public Order order;
    public ScoreTally tally;

    void Awake()
    {
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        order = GameObject.Find("DishPosition").GetComponent<Order>();
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
    }
    void Start()
    {
        currentTab = 5f;
        tab1Slider.value = currentTab;
        tab2Slider.value = currentTab;
        if (tally.LvlCompCount == 3){
        tab3Slider.value = currentTab;
        }
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
        
        if (tally.LvlCompCount == 3){
        if (itPick.table3Placed)
        {
            t3Bar.gameObject.SetActive(true);
            StartCoroutine(DepleteTableBar(t2Bar));
        }
        }
    }

    public IEnumerator DepleteTableBar(Canvas bar)
    {
        yield return new WaitForSeconds(1.5f);
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
            Destroy(itPick.dishInT1);
            agent = cusMove.customerInT1.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            bool hasReachedDest = false;

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (!hasReachedDest)
                    {
                        hasReachedDest = true;
                        Destroy(agent.gameObject);
                        spawnCus.cusCount--;
                        itPick.table1Placed = false;
                        Start();
                    }
                }
            }
            
        }
        if(bar.gameObject.name == "Table2Bar")
        {
            Destroy(itPick.dishInT2);
            agent = cusMove.customerInT2.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            bool hasReachedDest = false;
            
             if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (!hasReachedDest)
                    {
                        hasReachedDest = true;
                        Destroy(agent.gameObject);
                        spawnCus.cusCount--;
                        itPick.table2Placed = false;
                        Start();
                    }
                }
            }
        }
        if(bar.gameObject.name == "Table3Bar")
        {
            Destroy(itPick.dishInT3);
            agent = cusMove.customerInT3.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }
            Destroy(agent.gameObject);
            spawnCus.cusCount--;
            itPick.table3Placed = false;
            Start();
        }
    }

}
