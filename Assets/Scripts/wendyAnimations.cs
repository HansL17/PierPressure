using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wendyAnimations : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Transform childAnimator = transform.Find("wendy");
        if (childAnimator != null)
        {
            animator = childAnimator.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Child GameObject with Animator not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
