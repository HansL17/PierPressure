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

    //Int Values
    public int ExpertScore;
    public int UGCount;

    //Scripts
    public HUDCommands HudComm;
    public ItemPickup Serve;

    //Bools
    public bool PlantUG = false;
    public bool TableUG = false;
    public bool CarpetUG = false;


    // Start is called before the first frame update
    void Start()
    {
        HudComm = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();//Get HUDCommands Script
        Serve = GameObject.Find("Player").GetComponent<ItemPickup>(); //Get ItemPickup Script

        UGRemain.text = UGCount + " Remaining";

        DisableHUD();
        DisableUGObjects();
        GetUGButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableHUD() //Disable HUD method
    {
        HudComm.PauseScene();
        lvl2HUD.enabled = false; 
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
            PlantBtn.GetComponent<Image>().color = Color.green;
            UGCount--;
            PotPlant.SetActive(true);
        }
        UpdateUpgrade();
    }

    public void EnableTabMat()
    {
        if (UGCount > 0)
        {
            TableUG = true;
            TabMatBtn.GetComponent<Image>().color = Color.green;
            UGCount--;
            TabMat.SetActive(true);
            TabMat2.SetActive(true);
        }
        UpdateUpgrade();
    }

    public void EnableCarpet()
    {
        if (UGCount > 0)
        {
            CarpetUG = true;
            CarpetBtn.GetComponent<Image>().color = Color.green;
            UGCount--;
            Carpet.SetActive(true);
        }
        UpdateUpgrade();
    }

    public void ResetUG()
    {
        DisableUGObjects();
        UGCount = 1;
        UpdateUpgrade();
        PlantBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        TabMatBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        CarpetBtn.GetComponent<Image>().color = new Color32(239, 201, 170, 255);
        PlantUG = false;
        TableUG = false;
        CarpetUG = false;
    }

    public void UpdateUpgrade()
    {
        UGRemain.text = UGCount + " Remaining";
    }
}
