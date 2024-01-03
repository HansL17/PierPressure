using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class tBar : MonoBehaviour
{
    public GameObject sliderGO;
    public Canvas sliderCanv;
    public Slider slider;
    public bool startDepletion = false;
    public float depletionSpeed = 3.0f;

    [SerializeField] NavMeshAgent agent;

    private ItemPickup itPick;
    public CustomerMove cusMove;
    public SpawnCust spawnCus;
    public ScoreTally tally;

    void Awake()
    {
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>();

        sliderCanv = GetComponentInParent<Canvas>();
        sliderGO = sliderCanv.gameObject;
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
            if (startDepletion)
            {
                if (slider.value > 0)
                {
                    slider.value -= Time.deltaTime * depletionSpeed;
                }
                else
                {
                    StartCoroutine(TabGone());
                    startDepletion = false;
                }
            }
    }


    private IEnumerator TabGone()
        {
            if (sliderGO.name == "Table1Bar")
            {
                sliderGO.SetActive(false);
                slider.value = slider.maxValue;
                Destroy(itPick.dishInT1);
                agent = cusMove.customerInT1.GetComponent<NavMeshAgent>();
                Transform exit = GameObject.Find("customerExit").GetComponent<Transform>();
                agent.SetDestination(exit.transform.position);
                bool hasReachedDest = false;

                if(agent != null){
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
                        }
                    }
                }
                }
                
            }
            if(sliderGO.name == "Table2Bar")
            {
                Destroy(itPick.dishInT2);
                sliderGO.SetActive(false);
                slider.value = slider.maxValue;
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
                        }
                    }
                }
            }
            if(sliderGO.name == "Table3Bar")
            {
                Destroy(itPick.dishInT3);
                sliderGO.SetActive(false);
                slider.value = slider.maxValue;
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
                            }
                        }
                    }
                }



            }
        }
}
