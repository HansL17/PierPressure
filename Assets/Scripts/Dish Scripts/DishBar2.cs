using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishBar2 : MonoBehaviour
{
    [SerializeField] private Slider dishTime;
    [SerializeField] private GameObject _button2;

    public float maxTimer = 5f; //max patience (in seconds)
    public float currentTimer; //current patience (in seconds)

    public DishSpawn2 dishSpawn2;

    private void Awake()
    {
        dishSpawn2 = GameObject.Find("DishButton2").GetComponent<DishSpawn2>(); //Get script
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
        dishTime.value = currentTimer;
        //Start the patience countup

    }

    public IEnumerator IncreaseDishBar2()
    {
        while (currentTimer < 5f)
        {
            currentTimer += Time.deltaTime;
            dishTime.value += Time.deltaTime;
            yield return null;
        }


        //When Dish bar is done, perform any necessary action with PatienceGone()
        TimerDone();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void TimerDone()
    {
        dishTime.value = 0;
        currentTimer = 0;
    }
}
