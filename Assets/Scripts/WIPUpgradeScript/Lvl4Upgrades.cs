using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Lvl4Upgrades : MonoBehaviour
{
    //Canvases
    public Canvas lvl4Upgrade;
    public Canvas lvl4HUD;
    public Image lvl4Desc;
    public GameObject lvl4Popups;

    //Texts
    public TextMeshProUGUI UGRemain;

    //Wendy
    [SerializeField] NavMeshAgent player;

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
    public GameObject wendy;
    public GameObject WShoesApron;

    //Upgrade buttons
    public GameObject FLightsBtn;
    public GameObject LanternBtn;
    public GameObject FlrLightsBtn;

    //Descriptions
    public GameObject FLightsDesc;
    public GameObject LanternDesc;
    public GameObject FlrLightsDesc;

    //Dish Spawn Bar
    [SerializeField] Slider Dishbar1;
    [SerializeField] Slider Dishbar2;

    //Int Values
    public int UGCount = 0;

    //Scripts
    public SpawnCust cusSpawn;
    public DishSpawn DSpawn;
    public DishSpawn2 DSpawn2;
    public HUDCommands HCom;
    public ScoreTally Tally4;
    public SoundScript upBGM;

    //Bools
    public bool FairyLights;
    public bool LanternUP;
    public bool FloorLights;

    //Button Highlight color
    private Color highlight;
    private string hexColor = "#C4DEA4";

    void Awake()
    {
        GetScripts();
        upBGM.UpgBGM.Play();
        DisableUGObjects3();
    }

    // Start is called before the first frame update
    void Start()
    {
        lvl4Upgrade.enabled = false;
        lvl4HUD.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableUpgrade()
    {
        lvl4Popups.SetActive(false);
        lvl4Upgrade.enabled = true;
        Functions();
    }

    public void Functions()
    {

        Downgrades();
        ExScoreCheck3();
        UpdateUpgrade3();
        DisableHUD2();
        GetUGButtons3();
        UGCheck();
    }

    public void DisableHUD2()
    {
        HCom.PauseScene();
        lvl4HUD.enabled = false;
        lvl4Desc.enabled = false;

        FLightsDesc.SetActive(false);
        LanternDesc.SetActive(false);
        FlrLightsDesc.SetActive(false);

    }

    public void GetScripts()
    {
        HCom = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();//Get HUDCommands Script
        cusSpawn = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); // Get SpawnCust Script
        DSpawn = GameObject.Find("DishButton").GetComponent<DishSpawn>(); //Get DishSpawn script
        DSpawn2 = GameObject.Find("DishButton2").GetComponent<DishSpawn2>(); //Get DishSpawn2 script
        Tally4 = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();//Get ScoreTally Script
        upBGM = GameObject.Find("SoundDesign").GetComponent<SoundScript>();
    }

    public void UGCheck()
    {
        if (Tally4.PlantUPG == true)
        {
            PotPlant.SetActive(true);
        }
        if (Tally4.TableUPG == true)
        {
            TabMat.SetActive(true);
            TabMat2.SetActive(true);
            TabMat3.SetActive(true);
        }
        if (Tally4.CarpetUPG == true)
        {
            Carpet.SetActive(true);
        }
        if (Tally4.ShoesUPG == true)
        {
            wendy.SetActive(false);
            if (Tally4.ApronUPG == true)
            {
                WShoesApron.SetActive(true);
            }
            else
            {
                WShoes.SetActive(true);
            }
        }
        if (Tally4.ApronUPG == true)
        {
            wendy.SetActive(false);
            if (Tally4.ShoesUPG == true)
            {
                WShoesApron.SetActive(true);
            }
            else
            {
                WApron.SetActive(true);
            }
        }
        if (Tally4.JarsUPG == true)
        {
            MJars.SetActive(true);
        }
    }

    public void ExScoreCheck3()
    {
        if (Tally4.ExScoreCount >= 3)
        {
            UGCount = 2;
        }
        else
        {
            UGCount = 1;
        }
    }

    public void UpdateUpgrade3()
    {
        UGRemain.text = UGCount + " Remaining";
    }

    public void DisableUGObjects3()
    {
        //Bools for Lvl4 Upgrades
        FairyLights = false;
        LanternUP = false;
        FloorLights = false;

        //GameObjects for Lvl4 Upgrades
        FLights.SetActive(false);
        Lanterns.SetActive(false);
        FlrLights.SetActive(false);
        

        //Lvl2 upgrades
        PotPlant.SetActive(false);
        TabMat.SetActive(false);
        TabMat2.SetActive(false);
        Carpet.SetActive(false);

        //Lvl3 upgrades
        TabMat3.SetActive(false);
        WShoes.SetActive(false);
        WApron.SetActive(false);
        MJars.SetActive(false);
    }

    public void EnableHUD3()
    {
        if (UGCount == 0)
        {
            upBGM.UpgBGM.Stop();
            upBGM.PlayBGM();
            HCom.ResumeScene();
            lvl4Upgrade.enabled = false;
            lvl4HUD.enabled = true;
        }

    }

    public void GetUGButtons3()
    {
        //Reference buttons for upgrades
        FLightsBtn = GameObject.Find("FLightsBtn");
        LanternBtn = GameObject.Find("LanternBtn");
        FlrLightsBtn = GameObject.Find("FlrLightsBtn");

    }

    public void EnableFLights()
    {
        if (UGCount > 0)
        {
            FLights.SetActive(true);
            FairyLights = true;
            Tally4.FLightsUPG = true;
            lvl4Desc.enabled = true;
            FlrLightsDesc.SetActive(false);
            LanternDesc.SetActive(false);
            FLightsDesc.SetActive(true);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            FLightsBtn.GetComponent<Image>().color = highlight;
            UGCount--;
            //Speed revert code
            if (Tally4.ShoesUPG == true)
            {
                player.speed = 7.0f;
            }
            else
            {
                player.speed = 5.0f;
            }
            
        }

        UpdateUpgrade3();
    }

    public void EnableLanterns()
    {
        if (UGCount > 0)
        {
            Lanterns.SetActive(true);
            LanternUP = true;
            Tally4.LanternsUPG = true;
            lvl4Desc.enabled = true;
            FlrLightsDesc.SetActive(false);
            LanternDesc.SetActive(true);
            FLightsDesc.SetActive(false);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            LanternBtn.GetComponent<Image>().color = highlight;
            UGCount--;
            //Revert dish spawning
            if(Tally4.JarsUPG == true)
            {
                Dishbar1.maxValue = 4;
                DSpawn.spawnDelay = 4;
                Dishbar2.maxValue = 4;
                DSpawn2.spawnDelay = 4;
            }
            else
            {
                Dishbar1.maxValue = 5;
                DSpawn.spawnDelay = 5;
                Dishbar2.maxValue = 5;
                DSpawn2.spawnDelay = 5;
            }
        }

        UpdateUpgrade3();
    }

    public void EnableFlrLights()
    {
        if (UGCount > 0)
        {
            FlrLights.SetActive(true);
            FloorLights = true;
            Tally4.FlrLightsUPG = true;
            lvl4Desc.enabled = true;
            FlrLightsDesc.SetActive(true);
            LanternDesc.SetActive(false);
            FLightsDesc.SetActive(false);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            FlrLightsBtn.GetComponent<Image>().color = highlight;
            UGCount--;
            //Revert Customer Speed
            if(Tally4.CarpetUPG == true)
            {
                cusSpawn.timeDelay = 4f;
            }
            else
            {
                cusSpawn.timeDelay = 5f;
            }

        }

        UpdateUpgrade3();
    }

    public void Downgrades()
    {
        //Wendy speed regress
        player.speed = 4.0f;
        //Dish spawn regress
        Dishbar1.maxValue = 6;
        DSpawn.spawnDelay = 6;
        Dishbar2.maxValue = 6;
        DSpawn2.spawnDelay = 6;
        //Customer Spawn regress
        cusSpawn.timeDelay = 6f;
    }

    public void ResetUG3()
    {
        DisableUGObjects3();
        FLightsBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        LanternBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        FlrLightsBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        EnableUpgrade();
        UGCheck();
    }
}
