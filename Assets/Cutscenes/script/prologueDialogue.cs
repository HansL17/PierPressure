using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class prologueDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public Sprite[] pics; 
    private Sprite personTalking;
    public Image person;
    public GameObject personGameObj;
    public GameObject humbleBeginnings;
    public AudioSource typeSound;
    public bool Typing = false;
    public bool isPaused = false;
    private int index;
   

    // Start is called before the first frame update
    void Start()
    {
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
                }
            }
        }

        if (index <= 1 || index == 3)
        {
            personTalking = pics[3];
        }
        if (index == 2)
        {
            personTalking = pics[9];
        }
        if (index == 4 || index == 6)
        {
            personTalking = pics[8];
        }
        if (index == 5 || index == 17 || index == 21 || index == 23)
        {
            personTalking = pics[0];
        }
        if (index == 7 || index == 9 || index == 19)
        {
            personTalking = pics[4];
        }
        if (index == 8 || index == 10)
        {
            personTalking = pics[10];
        }
        if (index == 11 || index == 22)
        {
            personTalking = pics[14];
        }
        if (index == 12)
        {
            personTalking = pics[13];
        }
        if (index == 13)
        {
            personTalking = pics[16];
        }
        if (index == 14)
        {
            personTalking = pics[5];
        }
        if (index == 15)
        {
            personTalking = pics[15];
        }
        if (index == 16)
        {
            personTalking = pics[11];
        }
        if (index == 18)
        {
            personTalking = pics[7];
        }
        if (index == 20)
        {
            personTalking = pics[12];
        }
        if (index == 24)
        {
            personTalking = pics[2];
        }
        if (index == 25)
        {
            personTalking = pics[1];
        }
        if (index == 26)
        {
            personTalking = pics[5];
            humbleBeginnings.SetActive(true);
        }
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