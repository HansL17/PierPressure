using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public GameObject wheel;
    public float globalTime = 240;
    private float remainingRotation = 110.0f;
    public bool isRotating = false;
    private float rotationSpeed = 0.5f;
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
        if (globalTime < 240)
        {
            isRotating = true;
        }
    }
}
