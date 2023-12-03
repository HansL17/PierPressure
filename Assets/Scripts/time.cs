using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time : MonoBehaviour
{
    public Light sun;
    public Color targetColor;
    public Color currentColor;
    private Color initialColor;
    public TextMeshProUGUI timeText;
    public GameObject wheel;
    public float globalTime = 240;
    private float remainingRotation = 110.0f;
    public bool isRotating = false;
    private float rotationSpeed = 0.5f;
    public string activeSceneName;
    void Awake()
    {
         activeSceneName = Application.loadedLevelName;
    }

    void Start()
    {
        if (sun != null)
        {
            initialColor = sun.color;
            StartCoroutine(ChangeColorOverTime());
        }
         else
        {
            Debug.LogError("Light source not assigned!");
        }
    }
    void Update()
    {
        if (globalTime > 0){
            globalTime -= Time.deltaTime;
            float rotationSpeed = remainingRotation / globalTime;

            if (isRotating)
            {
                wheel.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
            else
            {
                wheel.transform.rotation = Quaternion.identity;
            }
        }
        else{
            globalTime = 0;
            isRotating = false;
            wheel.transform.rotation = Quaternion.identity;
        }

        int hour = Mathf.FloorToInt(globalTime / 30);
        
        if (activeSceneName == "Tutorial Level"){
        switch (hour)
        {
            case 7:
                timeText.text = "9:00 AM";
                break;
            case 6:
                timeText.text = "10:00 AM";
                break;
            case 5:
                timeText.text = "11:00 AM";
                break;
            case 4:
                timeText.text = "12:00 PM";
                break;
            case 3:
                timeText.text = "1:00 PM";
                break;
            case 2:
                timeText.text = "2:00 PM";
                break;
            case 1:
                timeText.text = "3:00 PM";
                break;
            default:
                timeText.text = "4:00 PM";
                break;
        }
        }

        if (activeSceneName == "Level2"){
            switch (hour)
            {
            case 7:
                timeText.text = "10:00 AM";
                break;
            case 6:
                timeText.text = "11:00 AM";
                break;
            case 5:
                timeText.text = "12:00 PM";
                break;
            case 4:
                timeText.text = "1:00 PM";
                break;
            case 3:
                timeText.text = "2:00 PM";
                break;
            case 2:
                timeText.text = "3:00 PM";
                break;
            case 1:
                timeText.text = "4:00 PM";
                break;
            default:
                timeText.text = "5:00 PM";
                break;
            }
        }

        if (activeSceneName == "Level3"){
            switch (hour)
            {
            case 7:
                timeText.text = "11:00 AM";
                break;
            case 6:
                timeText.text = "12:00 PM";
                break;
            case 5:
                timeText.text = "1:00 PM";
                break;
            case 4:
                timeText.text = "2:00 PM";
                break;
            case 3:
                timeText.text = "3:00 PM";
                break;
            case 2:
                timeText.text = "4:00 PM";
                break;
            case 1:
                timeText.text = "5:00 PM";
                break;
            default:
                timeText.text = "6:00 PM";
                break;
            }
        }

        if (activeSceneName == "Level4" || activeSceneName == "Level5"){
            switch (hour)
            {
            case 7:
                timeText.text = "12:00 PM";
                break;
            case 6:
                timeText.text = "1:00 PM";
                break;
            case 5:
                timeText.text = "2:00 PM";
                break;
            case 4:
                timeText.text = "3:00 PM";
                break;
            case 3:
                timeText.text = "4:00 PM";
                break;
            case 2:
                timeText.text = "5:00 PM";
                break;
            case 1:
                timeText.text = "6:00 PM";
                break;
            default:
                timeText.text = "7:00 PM";
                break;
            }
        }

        if (globalTime < 240)
        {
            isRotating = true;
        }
    }
    
    private IEnumerator ChangeColorOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 240)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / 240);
            sun.color = Color.Lerp(initialColor, targetColor, t);
            currentColor = sun.color;

            yield return null;
        }
        sun.color = targetColor;
    }
}
