using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTally : MonoBehaviour
{
    //GameObject To Store
    public GameObject TallyOBJ;

    //Bool for EXScore
    public bool ExpertScore = false;

    //Bool for Failed Level
    public bool NoPatience = false;

    //Bool for Level5
    public bool isLevel5 = false;

    //Integers
    public int ExScoreCount;
    public int LvlCompCount;

    //Upgrades Level 2
    public bool TableUPG;
    public bool PlantUPG;
    public bool CarpetUPG;

    //Upgrades Level 3
    public bool ShoesUPG;
    public bool ApronUPG;
    public bool JarsUPG;

    //Upgrades Level 4
    public bool FLightsUPG;
    public bool LanternsUPG;
    public bool FlrLightsUPG;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(TallyOBJ);
    }

}
