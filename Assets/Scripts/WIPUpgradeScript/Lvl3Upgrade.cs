using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Lvl3Upgrade : MonoBehaviour
{
    //Canvases
    public Canvas lvl3Upgrade;
    public Canvas lvl3HUD;

    //Texts
    public TextMeshProUGUI UGRemain;

    //Wendy
    [SerializeField] NavMeshAgent player;

    //Upgrade Assets Lvl3
    public GameObject WShoes = null;
    public GameObject WApron = null;
    public GameObject MJars = null;

    //Lvl2 Upgrades
    public GameObject PotPlant;
    public GameObject TabMat;
    public GameObject TabMat2;
    public GameObject TabMat3;
    public GameObject Carpet;

    //Upgrade buttons
    public GameObject ShoesBtn;
    public GameObject ApronBtn;
    public GameObject JarsBtn;

    //Descriptions
    public GameObject shoesDesc;
    public GameObject apronDesc;
    public GameObject jarsDesc;

    //Dish Spawn Bar
    [SerializeField] Slider Dishbar1;

    //Int Values
    public int UGCount = 0;

    //Scripts
    public ScoreTally Tally3;
    public HUDCommands HCom;
    public DishSpawn DSpawn;
    public Scoring Score2;

    //Bools
    public bool WenShoes;
    public bool WenApron;
    public bool MasJars;

    void Start()
    {
        UGCheck();
        DisableUGObjects2();
        GetScripts();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableHUD2()
    {
        HCom.PauseScene();
        lvl3Upgrade.enabled = true;
        lvl3HUD.enabled = false;
    }

    public void GetScripts()
    {
        HCom = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();//Get HUDCommands Script
        Tally3 = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();//Get ScoreTally Script
        DSpawn = GameObject.Find("DishButton").GetComponent<DishSpawn>(); //Get DishSpawn script
        Score2 = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get Scoring Script
    }

    public void UGCheck()
    {
        if (Tally3.TableUPG == true)
        {
            PotPlant.SetActive(true);
        }
        else if (Tally3.TableUPG == true)
        {
            TabMat.SetActive(true);
            TabMat2.SetActive(true);
            TabMat3.SetActive(true);
        }
        else if (Tally3.CarpetUPG == true)
        {
            Carpet.SetActive(true);
        }
    }

    public void ExScoreCheck2()
    {
        if(Tally3.ExScoreCount >= 2)
        {
            UGCount = 2;
        }
        else if(Tally3.ExScoreCount >= 1)
        {
            UGCount = 1;
        }
    }

    public void UpdateUpgrade2()
    {
        UGRemain.text = UGCount + " Remaining";
    }

    public void DisableUGObjects2()
    {
        WenShoes = false;
        WenApron = false;
        MasJars = false;

        ShoesBtn.SetActive(false);
        ApronBtn.SetActive(false);
        JarsBtn.SetActive(false);

        //Table mat 3
        TabMat3.SetActive(false);
    }

    public void DisableHUD3()
    {
        HCom.PauseScene();
        lvl3HUD.enabled = false;

    }

    public void EnableHUD2()
    {
        HCom.ResumeScene();
        lvl3Upgrade.enabled = false;
        lvl3HUD.enabled = true;
    }

    public void GetUGButtons2()
    {
        //Reference buttons for upgrades
    }

    public void EnableShoes()
    {
        if(UGCount > 0)
        {
            WShoes.SetActive(true);
            WenShoes = true;
            UGCount--;
            //Speed upgrade code
            player.speed = 7.0f;
        }
        

        UpdateUpgrade2();
    }

    public void EnableApron()
    {
        if (UGCount > 0)
        {
            WApron.SetActive(true);
            WenApron = true;
            UGCount--;
            //30% chance for Scoring to add +5 pts
            if (Random.value < 0.3f)
            {
                Score2.ScoreInc += 5; // If true, add 5 to the points
                Debug.Log("Bonus Points Success! +5");
            }
            else
            {
                Debug.Log("Bonus Points Failed! Better Luck Next Time!");
            }
        }

        UpdateUpgrade2();
    }

    public void EnableJars()
    {
        if (UGCount > 0)
        {
            MJars.SetActive(true);
            MasJars = true;
            UGCount--;
            //faster dish spawning
            Dishbar1.maxValue = 4;
            DSpawn.spawnDelay = 4;
        }

        UpdateUpgrade2();
    }

    public void ResetUG2()
    {
        DisableUGObjects2();
        ExScoreCheck2();
        UpdateUpgrade2();
    }
}
