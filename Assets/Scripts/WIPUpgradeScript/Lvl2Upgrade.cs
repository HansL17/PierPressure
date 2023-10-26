using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lvl2Upgrade : MonoBehaviour
{

    public Canvas lvl2Upgrade;
    public Canvas lvl2HUD;

    public HUDCommands HudComm;


    // Start is called before the first frame update
    void Start()
    {
        HudComm = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();//Get HUDCommands Script

        HudComm.PauseScene();
        DisableHUD();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableHUD()
    {
        lvl2HUD.enabled = false;
    }

    public void EnableHUD()
    {
        lvl2HUD.enabled = true;
        lvl2Upgrade.enabled = false;
        HudComm.ResumeScene();
    }
}
