using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public float timerValue;
    public bool isTimerActive = true, isTimerPaused = false;
    public TMP_Text displayTime;
    private float minutes;
    private float seconds;
    void Update()
    {
        if (!isTimerActive)
        {
            displayTime.text = "--:--";
            //TODO: figureout a way to add an infinty symbol instead of --:-- to display time.
        }
        if (isTimerActive)
        {
            if (timerValue > 0)
            {
                // TODO: pause timer if game is finished
                if (!isTimerPaused)
                {
                    timerValue -= Time.deltaTime;
                }
            }
            else
            {
                Debug.Log("Times up!");
                timerValue = 0;
                isTimerActive = false;
            }
            DisplayTime(timerValue);
        }
    }
    void DisplayTime(float timerToDisplay)
    {
        minutes = Mathf.FloorToInt(timerValue / 60);
        seconds = Mathf.FloorToInt(timerValue % 60);
        displayTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}