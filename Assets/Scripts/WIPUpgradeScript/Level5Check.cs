using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Check : MonoBehaviour
{

    public ScoreTally Tally5;

    //Upgrade Assets Lvl4
    public GameObject FLights;
    public GameObject Lanterns;
    public GameObject FlrLights;

    //Lvl2 Upgrades
    public GameObject PotPlant;
    public GameObject TabMat;
    public GameObject TabMat2;
    public GameObject TabMat3;
    public GameObject Carpet;

    //Lvl3 Upgrades
    public GameObject WShoes;
    public GameObject WApron;
    public GameObject MJars;


    void Awake()
    {
        Tally5 = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        Tally5.LvlCompCount++;
    }
    void Start()
    {
        UGCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UGCheck()
    {
        if (Tally5.PlantUPG == true)
        {
            PotPlant.SetActive(true);
        }
        if (Tally5.TableUPG == true)
        {
            TabMat.SetActive(true);
            TabMat2.SetActive(true);
            TabMat3.SetActive(true);
        }
        if (Tally5.CarpetUPG == true)
        {
            Carpet.SetActive(true);
        }
        if (Tally5.ShoesUPG == true)
        {
            WShoes.SetActive(true);
        }
        if (Tally5.ApronUPG == true)
        {
            WApron.SetActive(true);
        }
        if (Tally5.JarsUPG == true)
        {
            MJars.SetActive(true);
        }
        if (Tally5.FLightsUPG == true)
        {
            FLights.SetActive(true);
        }
        if (Tally5.LanternsUPG == true)
        {
            Lanterns.SetActive(true);
        }
        if (Tally5.FlrLightsUPG == true)
        {
            FlrLights.SetActive(true);
        }
    }
}

