using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class customerAnimation : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public CustomerMove cusMove;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        cusMove = cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (agent.gameObject == cusMove.customerInT1 || agent.gameObject == cusMove.customerInT2 || agent.gameObject == cusMove.customerInT3)
        {
            StartCoroutine(CustomerSit());
        }
        bool walk = animator.GetBool("isWalking");
        if (walk == true)
        {
            animator.SetBool("isSitting", false);
        }


    }

    private IEnumerator CustomerSit() {
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        animator.SetBool("isSitting", true);        
    }
}
