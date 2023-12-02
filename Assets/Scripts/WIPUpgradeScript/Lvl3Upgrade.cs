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
    public Image lvl3Desc;

    //Texts
    public TextMeshProUGUI UGRemain;

    //Wendy
    [SerializeField] NavMeshAgent player;

    //Upgrade Assets Lvl3
    public GameObject WShoes = null;
    public GameObject WApron = null;
    public GameObject MJars = null;
    public GameObject wendy;
    public GameObject WShoesApron;

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
    [SerializeField] Slider Dishbar2;

    //Int Values
    public int UGCount = 0;

    //Scripts
    public ScoreTally Tally3;
    public HUDCommands HCom;
    public DishSpawn DSpawn;
    public DishSpawn2 DSpawn2;
    public Scoring Score2;
    public SoundScript upBGM;

    //Bools
    public bool WenShoes;
    public bool WenApron;
    public bool MasJars;

    //Button Highlight color
    private Color highlight;
    private string hexColor = "#C4DEA4";

    void Awake()
    {
        GetScripts();
        Tally3.LvlCompCount++;
    }

    void Start()
    {
        ExScoreCheck2();
        UpdateUpgrade2();
        DisableHUD2();
        DisableUGObjects2();
        GetUGButtons2();
        UGCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableHUD2()
    {
        HCom.PauseScene();
        upBGM.UpgBGM.Play();
        lvl3HUD.enabled = false;
        lvl3Desc.enabled = false;

        shoesDesc.SetActive(false);
        apronDesc.SetActive(false);
        jarsDesc.SetActive(false);

    }

    public void GetScripts()
    {
        HCom = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();//Get HUDCommands Script
        Tally3 = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();//Get ScoreTally Script
        DSpawn = GameObject.Find("DishButton").GetComponent<DishSpawn>(); //Get DishSpawn script
        DSpawn2 = GameObject.Find("DishButton2").GetComponent<DishSpawn2>(); //Get DishSpawn2 script
        Score2 = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get Scoring Script
        upBGM = GameObject.Find("SoundDesign").GetComponent<SoundScript>();
    }

    public void UGCheck()
    {
        if (Tally3.PlantUPG == true)
        {
            PotPlant.SetActive(true);
        }
        if (Tally3.TableUPG == true)
        {
            TabMat.SetActive(true);
            TabMat2.SetActive(true);
            TabMat3.SetActive(true);
        }
        if (Tally3.CarpetUPG == true)
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
        else
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
        wendy.SetActive(true);
        WShoesApron.SetActive(false);
        WApron.SetActive(false);
        WShoes.SetActive(false);
        MJars.SetActive(false);

        PotPlant.SetActive(false);
        TabMat.SetActive(false);
        TabMat2.SetActive(false);
        TabMat3.SetActive(false);
        Carpet.SetActive(false);

        WenShoes = false;
        WenApron = false;
        MasJars = false;

        Tally3.ShoesUPG = false;
        Tally3.ApronUPG = false;
        Tally3.JarsUPG = false;

        apronDesc.SetActive(false);
        jarsDesc.SetActive(false);
        shoesDesc.SetActive(false);
    }


    public void EnableHUD2()
    {
        if (UGCount == 0)
        {
            upBGM.UpgBGM.Stop();
            upBGM.PlayBGM();
            HCom.ResumeScene();
            lvl3Upgrade.enabled = false;
            lvl3HUD.enabled = true;
        }
        
    }

    public void GetUGButtons2()
    {
        //Reference buttons for upgrades
        ShoesBtn = GameObject.Find("ShoeBtn");
        ApronBtn = GameObject.Find("ApronBtn");
        JarsBtn = GameObject.Find("MJarsBtn");

    }

    public void EnableShoes()
    {
        if(UGCount > 0)
        {
            wendy.SetActive(false);
            if(WenApron == true){
                WApron.SetActive(false);
                WShoesApron.SetActive(true);
            } else {
                WShoes.SetActive(true);
            }
            WenShoes = true;
            Tally3.ShoesUPG = true;
            lvl3Desc.enabled = true;
            //Descriptions
            shoesDesc.SetActive(true);
            apronDesc.SetActive(false);
            jarsDesc.SetActive(false);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            ShoesBtn.GetComponent<Image>().color = highlight;
            //UG Count Decrement
            UGCount--;
            shoesDesc.SetActive(true);
            //Speed upgrade code
            player.speed = 7.0f;
        }
        UpdateUpgrade2();
    }

    public void EnableApron()
    {
        if (UGCount > 0)
        {
            wendy.SetActive(false);
            if(WenShoes == true){
                WShoes.SetActive(false);
                WShoesApron.SetActive(true);
            } else {
                WApron.SetActive(true);
            }
            WenApron = true;
            Tally3.ApronUPG = true;
            lvl3Desc.enabled = true;
            //Descriptions
            shoesDesc.SetActive(false);
            apronDesc.SetActive(true);
            jarsDesc.SetActive(false);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            ApronBtn.GetComponent<Image>().color = highlight;
            //UG Count Decrement
            UGCount--;
            apronDesc.SetActive(true);
            //30% chance for Scoring to add +5 pts
        }
        UpdateUpgrade2();
    }

    public void EnableJars()
    {
        if (UGCount > 0)
        {
            MJars.SetActive(true);
            MasJars = true;
            Tally3.JarsUPG = true;
            lvl3Desc.enabled = true;
            //Descriptions
            shoesDesc.SetActive(false);
            apronDesc.SetActive(false);
            jarsDesc.SetActive(true);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            JarsBtn.GetComponent<Image>().color = highlight;
            //UG Count Decrement
            UGCount--;
            jarsDesc.SetActive(true);
            //faster dish spawning
            Dishbar1.maxValue = 4;
            DSpawn.spawnDelay = 4;
            Dishbar2.maxValue = 4;
            DSpawn2.spawnDelay = 4;
        }

        UpdateUpgrade2();
    }

    public void ResetUG2()
    {
        DisableUGObjects2();
        ExScoreCheck2();
        UpdateUpgrade2();
        UGCheck();
        lvl3Desc.enabled = false;
        ShoesBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        ApronBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        JarsBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
    }
}
