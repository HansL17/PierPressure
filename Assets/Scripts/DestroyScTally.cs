using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScTally : MonoBehaviour
{
    public ScoreTally tally;
    // Start is called before the first frame update
    void Start()
    {
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyFinalTally()
    {
        tally.NoPatience = false;
        Destroy(tally.gameObject);
    }
}
