using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class ending2Dialogue : MonoBehaviour
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
        if (index == 0 || index == 3 || index >= 16)
        {
            personTalking = pics[0];
        }
        if (index == 1)
        {
            personTalking = pics[1];
        }
        if (index == 2 || index == 8 || index == 15)
        {
            personTalking = pics[2];
        }
        if (index == 4)
        {
            personTalking = pics[3];
        }
        if (index == 5)
        {
            personTalking = pics[10];
        }
        if (index == 6 || index == 10)
        {
            personTalking = pics[4];
        }
        if (index == 12)
        {
            personTalking = pics[5];
        }
        if (index == 7 || index == 14)
        {
            personTalking = pics[6];
        }
        if (index == 9)
        {
            personTalking = pics[7];
        }
        if (index == 11)
        {
            personTalking = pics[8];
        }
        if (index == 13)
        {
            personTalking = pics[9];
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