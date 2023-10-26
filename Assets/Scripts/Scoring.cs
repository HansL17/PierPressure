using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextLVLdoneCOMP;
    public TextMeshProUGUI scoreTextLVLdoneFAIL;
    public TextMeshProUGUI scoreType;
    public GameObject lvlComp;
    public GameObject lvlFail;
    public GameObject HUD;

    public int score = 0;
    public int normalScore;
    public int expertScore;

    public bool isAction1Done = false;
    public bool isAction2Done = false;
    public int consecutiveActions1 = 0;
    public int consecutiveActions2 = 0;

    public SpawnCust spawnCus;
    public HUDCommands hud;

    void Awake()
    {
        hud = GameObject.Find("CanvasMAIN").GetComponent<HUDCommands>();
        spawnCus = GameObject.Find("CustomerSpawn").GetComponent<SpawnCust>(); //Get script
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddScore(int newScore) // Function for adding score
    {
        if (consecutiveActions1 >= 1)
        {
            newScore *= 2; // Increase the score by 20 if one action is done twice in a row
            score += newScore;
            consecutiveActions1 = 0; // Reset the consecutive actions count
            Debug.Log("Combo x2");
        }
        else if (consecutiveActions2 >= 2)
        {
            newScore *= 2; // Increase the score by 20 if one action is done twice in a row
            score += newScore;
            consecutiveActions2 = 0; // Reset the consecutive actions count
            Debug.Log("Combo x2");
        }
        else
        {
            score += newScore;
        }
    }

    public void SubScore(int newScore) // Function for subtracting score
    {
        score -= newScore;
    }

    public void UpdateScore() // Function for updating score
    {
        scoreText.text = "Score: " + score;

        if (score == normalScore)
        {
            Debug.Log("Normal Score Achieved!");
        }
        else if (score == expertScore)
        {
            Debug.Log("Expert Score Achieved!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();

        if(spawnCus.totalCus == 6 && spawnCus.cusCount == 0)
        {
            hud.PauseScene();
            if(expertScore > score && score >= normalScore)
            {
                HUD.gameObject.SetActive(false);
                lvlComp.gameObject.SetActive(true);
                scoreTextLVLdoneCOMP.text = "Score: " + score;
                scoreType.text = "Normal Score Achieved!";
            } else if (score >= expertScore)
            {
                HUD.gameObject.SetActive(false);
                lvlComp.gameObject.SetActive(true);
                scoreTextLVLdoneCOMP.text = "Score: " + score;
                scoreType.text = "Expert Score Achieved!";
            } else if (score < normalScore)
            {
                HUD.gameObject.SetActive(false);
                lvlFail.gameObject.SetActive(true);
                scoreTextLVLdoneFAIL.text = "Score: " + score;
            }
        }
    }
}
