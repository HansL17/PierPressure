using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lvl2Upgrade : MonoBehaviour
{
    //Canvases
    public Canvas lvl2Upgrade;
    public Canvas lvl2HUD;
    public Image lvl2Desc;

    //Texts
    public TextMeshProUGUI UGRemain;

    //Upgrade Assets
    public GameObject PotPlant;
    public GameObject TabMat;
    public GameObject TabMat2;
    public GameObject Carpet;

    //Upgrade buttons
    public GameObject PlantBtn;
    public GameObject TabMatBtn;
    public GameObject CarpetBtn;

    //Descriptions
    public GameObject plantDesc;
    public GameObject TabMatDesc;
    public GameObject CarpetDesc;

    //Int Values
    public int UGCount = 0;

    //Scripts
    public HUDCommands HudComm;
    public ItemPickup Serve;
    public ScoreTally Tally2;
    public Scoring Score;
    public CustomerMove cusMove;
    public SpawnCust cusSpawn;

    //Bools
    public bool PlantUG = false;
    public bool TableUG = false;
    public bool CarpetUG = false;

    //Button Highlight color
    private Color highlight;
    private string hexColor = "#C4DEA4";


    // Start is called before the first frame update
    void Start()
    {
        HudComm = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();//Get HUDCommands Script
        Serve = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get ItemPickup Script
        Score = GameObject.Find("ScoreUpdate").GetComponent<Scoring>(); //Get Scoring Script
        cusMove = GameObject.Find("CustomerLine").GetComponent<CustomerMove>(); // Get CustomerMove Script
        cusSpawn = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); // Get SpawnCust Script

        ExScoreCheck();
        DisableHUD();
        DisableUGObjects();
        GetUGButtons();

        UGRemain.text = UGCount + " Remaining";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableHUD() //Disable HUD method
    {
        HudComm.PauseScene();
        lvl2HUD.enabled = false;
        lvl2Desc.enabled = false;
        plantDesc.SetActive(false);
        TabMatDesc.SetActive(false);
        CarpetDesc.SetActive(false);
    }

    public void EnableHUD() //Enable HUD method
    {
        if(UGCount == 0)
        {
            lvl2HUD.enabled = true;
            lvl2Upgrade.enabled = false;
            HudComm.ResumeScene();
        }
    }

    public void DisableUGObjects()
    {
        PotPlant.SetActive(false);
        TabMat.SetActive(false);
        TabMat2.SetActive(false);
        Carpet.SetActive(false);
        PlantUG = false;
        Tally2.PlantUPG = false;
        TableUG = false;
        Tally2.TableUPG = false;
        CarpetUG = false;
        Tally2.CarpetUPG = false;
        plantDesc.SetActive(false);
        TabMatDesc.SetActive(false);
        CarpetDesc.SetActive(false);
    }

    public void GetUGButtons()
    {
        PlantBtn = GameObject.Find("PotPlantBtn");
        TabMatBtn = GameObject.Find("TabMatBtn");
        CarpetBtn = GameObject.Find("CarpetBtn");
    }

    public void EnablePlant()
    {
        if(UGCount > 0)
        {
            PlantUG = true;
            Tally2.PlantUPG = true;
            lvl2Desc.enabled = true;
            //Descriptions
            plantDesc.SetActive(true);
            TabMatDesc.SetActive(false);
            CarpetDesc.SetActive(false);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            PlantBtn.GetComponent<Image>().color = highlight;
            //UG Count Decrement
            UGCount--;
            PotPlant.SetActive(true);
            cusMove.speed = 6f;

        }
        UpdateUpgrade();
    }

    public void EnableTabMat()
    {
        if (UGCount > 0)
        {
            TableUG = true;
            Tally2.TableUPG = true;
            lvl2Desc.enabled = true;
            //Descriptions
            plantDesc.SetActive(false);
            TabMatDesc.SetActive(true);
            CarpetDesc.SetActive(false);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            TabMatBtn.GetComponent<Image>().color = highlight;
            UGCount--;
            TabMat.SetActive(true);
            TabMat2.SetActive(true);
            Score.ScoreInc = 15;
        }
        UpdateUpgrade();
    }

    public void EnableCarpet()
    {
        if (UGCount > 0)
        {
            CarpetUG = true;
            Tally2.CarpetUPG = true;
            lvl2Desc.enabled = true;
            //Descriptions
            plantDesc.SetActive(false);
            TabMatDesc.SetActive(false);
            CarpetDesc.SetActive(true);
            //Button Color
            ColorUtility.TryParseHtmlString(hexColor, out highlight);
            CarpetBtn.GetComponent<Image>().color = highlight;
            UGCount--;
            Carpet.SetActive(true);
            cusSpawn.timeDelay = 4f;
        }
        UpdateUpgrade();
    }

    public void ResetUG()
    {
        DisableUGObjects();
        ExScoreCheck();
        UpdateUpgrade();
        PlantBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        TabMatBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        CarpetBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        lvl2Desc.enabled = false;
        PlantUG = false;
        TableUG = false;
        CarpetUG = false;
        cusMove.speed = 5f;
        Score.ScoreInc = 10;
        cusSpawn.timeDelay = 5f;
    }

    public void UpdateUpgrade()
    {
        UGRemain.text = UGCount + " Remaining";
    }

    public void ExScoreCheck()
    {
        if (Tally2.ExScoreCount >= 1)
        {
            UGCount = 2;
        }
        else
        {
            UGCount = 1;
        }
    }
}
