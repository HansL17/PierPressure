using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableBar : MonoBehaviour
{
    [SerializeField] private Slider currentSlider;
    public Slider tab1Slider;
    public Slider tab2Slider;
    public float maxEat = 5f; //max patience (in seconds)
    public float currentTab; //current patience (in seconds)

    private ItemPickup itPick;
    private CustomerMove cusMove;

    void Awake()
    {
        itPick = GameObject.Find("Player").GetComponent<ItemPickup>();
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>();
    }
    void Start()
    {
        currentTab = 5f;
        tab1Slider.value = currentTab;
        tab2Slider.value = currentTab;
    }


    void Update()
    {
        if (itPick.table1Placed == true && cusMove.t1_occupied == true)
        {
            
            currentSlider = tab1Slider;
            StartCoroutine(DepleteTableBar());
        }

        if (itPick.table2Placed == true && cusMove.t2_occupied == true)
        {
            tab2Slider.gameObject.SetActive(true);
            currentSlider = tab2Slider;
            StartCoroutine(DepleteTableBar());
        } 
    }

    public IEnumerator DepleteTableBar()
    {
        while (currentTab > 0f)
        {
            currentTab -= Time.deltaTime;
            currentSlider.value -= Time.deltaTime;
            yield return null;
        }
        

        //When Patience bar is done, perform any necessary action with TabGone()
        TabGone();
    }

    private void TabGone()
    {
        Debug.Log("Customer is done eating");
        currentSlider.value = maxEat;
    }

}
