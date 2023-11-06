using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lvl3Upgrade : MonoBehaviour
{
    //Canvases
    public Canvas lvl3Upgrade;
    public Canvas lvl3HUD;

    //Texts
    public TextMeshProUGUI UGRemain;

    //Upgrade Assets
    public GameObject Upgrade1;
    public GameObject Upgrade2;
    public GameObject Upgrade3;

    //Lvl2 Upgrades
    public GameObject PotPlant;
    public GameObject TabMat;
    public GameObject TabMat2;
    public GameObject Carpet;

    //Upgrade buttons
    public GameObject UP1Btn;
    public GameObject UP2Btn;
    public GameObject UP3Btn;

    //Int Values
    public int UGCount = 0;

    //Scripts
    public ScoreTally Tally3;
    public HUDCommands HCom;

    //Bools
    public bool UPG1;
    public bool UPG2;
    public bool UPG3;

    void Start()
    {
        UGCheck();
        DisableUGObjects2();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        UPG1 = false;
        UPG2 = false;
        UPG3 = false;

        UP1Btn.SetActive(false);
        UP2Btn.SetActive(false);
        UP3Btn.SetActive(false);
    }

    public void DisableHUD2()
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
        
    }

    public void EnableUP1()
    {
        if(UGCount > 0)
        {
            Upgrade1.SetActive(true);
            UPG1 = true;
            UGCount--;
        }
        

        UpdateUpgrade2();
    }

    public void EnableUP2()
    {
        if (UGCount > 0)
        {
            Upgrade2.SetActive(true);
            UPG2 = true;
            UGCount--;
        }

        UpdateUpgrade2();
    }

    public void EnableUP3()
    {
        if (UGCount > 0)
        {
            Upgrade3.SetActive(true);
            UPG3 = true;
            UGCount--;
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
