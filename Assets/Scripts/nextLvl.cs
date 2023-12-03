using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLvl : MonoBehaviour
{

    public ScoreTally LvlTally;
    // Start is called before the first frame update
    void Awake()
    {
        LvlTally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
    }

    // Update is called once per frame
    public void NextLevel()
    {
        LvlTally.LvlCompCount++;
        if (LvlTally.LvlCompCount == 1)
        {
            SceneManager.LoadScene("Level2");
        }
        if (LvlTally.LvlCompCount == 2)
        {
            SceneManager.LoadScene("Level3");
        }
        if (LvlTally.LvlCompCount == 3)
        {
            SceneManager.LoadScene("Level4");
        }
        if(LvlTally.LvlCompCount == 4 && LvlTally.ExScoreCount == 4)
        {
            SceneManager.LoadScene("Level5");
        }
        if(LvlTally.LvlCompCount == 4 && LvlTally.ExScoreCount != 4)
        {
            SceneManager.LoadScene("Ending1");
        }
        if(LvlTally.LvlCompCount == 5 && LvlTally.ExScoreCount == 5)
        {
            SceneManager.LoadScene("Ending2");
        }
    }

    public void CheckScore()
    {
        if (LvlTally.ExpertScore == true)
        {
            LvlTally.ExScoreCount++;
            LvlTally.ExpertScore = false;
        }
    }
}
