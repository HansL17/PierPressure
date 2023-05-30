using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int normalScore;
    public int expertScore;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddScore(int newScore) //Function for adding score
    {
        score += newScore;
    }

    public void UpdateScore() //Function for updating score
    {
        scoreText.text = "Score " + score;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
}
