using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject _cus;

    public float maxPatience = 90f; //max patience (in seconds)
    public float currentPatience; //current patience (in seconds)

    public CustomerLine cusLine;

    private void Awake()
    {
        cusLine = GameObject.Find("CustomerSpawn").GetComponent<CustomerLine>(); //Get script
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPatience = 90;
        slider.value = currentPatience;
        StartCoroutine(DepletePatienceBar());
        //Start the patience countdown

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
        PatienceGone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void PatienceGone()
    {
        Debug.Log("Patience Depleted");
        _cus.SetActive(false);
        cusLine.UpdateLineup();
        
    }


}
