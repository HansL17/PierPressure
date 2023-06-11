using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PatienceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject _cus;

    public float maxPatience = 30f; //max patience (in seconds)
    public float currentPatience; //current patience (in seconds)
    public NavMeshAgent agent;

    private ItemPickup itPick;
    public CustomerLine cusLine;
    public SpawnCust spawnCus;


    private void Awake()
    {
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusLine = GameObject.Find("CustomerSpawn").GetComponent<CustomerLine>(); //Get script
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPatience = 90;
        slider.value = currentPatience;
        StartCoroutine(DepletePatienceBar());
        //Start the patience countdown
        agent = _cus.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(itPick.table1Placed || itPick.table2Placed)
        {
            GameObject toDelete = slider.gameObject;
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
        agent.SetDestination(exit.transform.position);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        Destroy(_cus);
        spawnCus.cusCount--;
    }
}
