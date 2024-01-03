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
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshAgent player;

    private ItemPickup itPick;
    public CustomerMove cusMove;
    public SpawnCust spawnCus;
    // public Order order;
    public ScoreTally tally;

    void Awake()
    {
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        //order = GameObject.Find("DishPosition").GetComponent<Order>();
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
    }
    void Start()
    {
        currentTab = 5f;
        tab1Slider.value = currentTab;
        tab2Slider.value = currentTab;
        if (tally.LvlCompCount >= 2){
        tab3Slider.value = currentTab;
        }
    }


    void Update()
    {
        if (itPick.dishInT1 != null)
        {
            StartCoroutine(DepleteTableBar(t1Bar));
            Debug.Log("Table 1 is Eating");
        }

        if (itPick.dishInT2 != null)
        {
            StartCoroutine(DepleteTableBar(t2Bar));
            Debug.Log("Table 2 is Eating");
        } 
        
        if (tally.LvlCompCount >= 2){
        if (itPick.table3Placed == true || itPick.dishInT3 != null)
        {
            StartCoroutine(DepleteTableBar(t3Bar));
                Debug.Log("Table 3 is Eating");
            }
        }
    }

    public IEnumerator DepleteTableBar(Canvas bar)
    {
        while (player.pathPending || player.remainingDistance > player.stoppingDistance)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (currentTab > 0f)
        {
            Slider slider = bar.GetComponentInChildren<Slider>();
            currentTab -= Time.deltaTime;
            slider.value -= Time.deltaTime;
            yield return null;
        }


        // //When Table bar is done, perform any necessary action with TabGone()
        if (currentTab <= 0)
        {
            StartCoroutine(TabGone(bar));
        }
    }

    private IEnumerator TabGone(Canvas bar)
    {
        if(agent != null)
        {
        Debug.Log("Customer is done eating");
        Slider slider = bar.GetComponentInChildren<Slider>();
        slider.value = 5f;
        }

        if (bar.gameObject.name == "Table1Bar")
        {
            Destroy(itPick.dishInT1);
            bar.gameObject.SetActive(false);
            agent = cusMove.customerInT1.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            bool hasReachedDest = false;

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f || agent.transform == exit)
                {
                    if (!hasReachedDest)
                    {
                        hasReachedDest = true;
                        Destroy(agent.gameObject);
                        spawnCus.cusCount--;
                        cusMove.customerInT1 = null;
                        itPick.dishInT1 = null;
                        itPick.table1Placed = false;
                        Start();
                    }
                }
            }
            
        }
        if(bar.gameObject.name == "Table2Bar")
        {
            Destroy(itPick.dishInT2);
            bar.gameObject.SetActive(false);
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
                        cusMove.customerInT2 = null;
                        itPick.dishInT2 = null;
                        itPick.table2Placed = false;
                        Start();
                    }
                }
            }
        }
        if(bar.gameObject.name == "Table3Bar")
        {
            Destroy(itPick.dishInT3);
            bar.gameObject.SetActive(false);
            agent = cusMove.customerInT3.GetComponent<NavMeshAgent>();
            Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
            agent.SetDestination(exit.transform.position);
            bool hasReachedDest = false;
            if (agent != null)
            {
                while (agent != null && (agent.pathPending || agent.remainingDistance > agent.stoppingDistance))
                {
                    yield return null;
                }

                if (agent != null && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        if (!hasReachedDest)
                        {
                            hasReachedDest = true;
                            spawnCus.cusCount--;
                            cusMove.customerInT3 = null;
                            itPick.dishInT3 = null;
                            itPick.table3Placed = false;
                            Start();
                        }
                    }
                }
            }



        }
    }

}
