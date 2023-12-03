using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wendyAnimations : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private Transform wendyAnimator;
    private Transform wendyShoesAnimator;
    private Transform wendyApronAnimator;
    private Transform wendyBothAnimator;

    public ItemPickup itemPick;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        wendyAnimator = transform.Find("wendy");
        wendyShoesAnimator = transform.Find("wendyNike");
        wendyApronAnimator = transform.Find("wendyapron");
        wendyBothAnimator = transform.Find("wendyapronandshoes"); 

        itemPick = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get script
    }

    // Update is called once per frame
    void Update()
    {
        if (wendyAnimator.gameObject.activeSelf)
        {
            animator = wendyAnimator.GetComponent<Animator>();
        }
        else if (wendyShoesAnimator.gameObject.activeSelf)
        {
            animator = wendyShoesAnimator.GetComponent<Animator>();
        }
        else if (wendyApronAnimator.gameObject.activeSelf)
        {
            animator = wendyApronAnimator.GetComponent<Animator>();
        }
        else if (wendyBothAnimator.gameObject.activeSelf)
        {
            animator = wendyBothAnimator.GetComponent<Animator>();
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (itemPick.isHoldingItem == true){
            animator.SetBool("IsHolding", true);
        }
        else{
            animator.SetBool("IsHolding", false);
        }
    }
}
