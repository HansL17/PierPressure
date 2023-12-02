using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Check : MonoBehaviour
{

    public ScoreTally Tally4;


    void Awake()
    {
        Tally4 = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        Tally4.LvlCompCount++;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
