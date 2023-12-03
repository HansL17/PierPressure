using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class prologueDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject[] names;

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
            }
        }

        if (index <= 1 || index == 3 || index == 5 || index == 7 || index == 9 || index == 14 || index == 17 || index == 19 || index == 21 || index >= 23)
        {
            names[0].SetActive(true);
            names[1].SetActive(false);
            names[2].SetActive(false);
            names[3].SetActive(false);
        }
        if (index ==2 || index == 4 || index == 6 || index == 18)
        {
            names[0].SetActive(false);
            names[1].SetActive(true);
            names[2].SetActive(false);
            names[3].SetActive(false);
        }
        if (index == 8 || index == 10 || index ==12 || index ==16 || index ==20)
        {
            names[0].SetActive(false);
            names[1].SetActive(false);
            names[2].SetActive(true);
            names[3].SetActive(false);
        }
        if (index ==11 || index ==13 || index ==15 || index ==16 || index ==22)
        {
            names[0].SetActive(false);
            names[1].SetActive(false);
            names[2].SetActive(false);
            names[3].SetActive(true);
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
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
}