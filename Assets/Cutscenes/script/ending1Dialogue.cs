using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ending1Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public Sprite[] pics;
    private Sprite personTalking;
    public Image person;
    public GameObject personGameObj;
    public AudioSource typeSound;
    public bool Typing = false;
    public bool isPaused = false;
    public ScoreTally tally;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false){
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                    typeSound.Stop();
                    if (index == 5 && textComponent.text == lines[index])
                    {
                        SceneManager.LoadScene("mainMenu");
                        tally.ExScoreCount = 0;
                        tally.LvlCompCount = 0;
                    }
                }
            }
        }

        if (index == 0)
        {
            personTalking = pics[3];
        }
        if (index == 1 || index == 3)
        {
            personTalking = pics[1];
        }
        if (index == 2)
        {
            personTalking = pics[0];
        }
        if (index >= 4) {personTalking = pics[3];}
        person.sprite = personTalking;
        personGameObj.SetActive(true);
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        typeSound.Play();
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            if(textComponent.text == lines[index]){
                typeSound.Stop();
            }
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void trueBool()
    {
        isPaused = true;
    }

    public void falseBool()
    {
        isPaused = false;
    }
}