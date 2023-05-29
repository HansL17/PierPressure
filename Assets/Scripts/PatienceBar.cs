using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public float maxPatience = 180f; //max patience (in seconds)
    public float currentPatience; //current patience (in seconds)

    // Start is called before the first frame update
    void Start()
    {
        currentPatience = 180;
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
        Destroy(gameObject);
    }


}
