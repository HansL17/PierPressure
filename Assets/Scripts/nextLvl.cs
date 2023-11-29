using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLvl : MonoBehaviour
{

    public ScoreTally LvlTally;
    // Start is called before the first frame update
    void Start()
    {
        LvlTally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
    }

    // Update is called once per frame
    public void NextLevel()
    {
        if (LvlTally.LvlCompCount == 0)
        {
            SceneManager.LoadScene("Level2");
        }
        else if (LvlTally.LvlCompCount == 1)
        {
            SceneManager.LoadScene("Level3");
        }
        else if (LvlTally.LvlCompCount == 2)
        {
            SceneManager.LoadScene("Level4");
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
