using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Handler : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    void Start()
    {
        LevelController.onTimeChanged.AddListener(TimerTextHandler);
    }

    public void TimerTextHandler()
    {
        timerText.text = LevelController.SecondsLeft.ToString();
    }
}
