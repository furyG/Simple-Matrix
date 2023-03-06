using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action TimeChanged;

    private const int minTime = 0;
    private float roundTime;
    private float leftTime;

    public float LeftTime { get => leftTime; set => leftTime = value; }
    public float RoundTime => roundTime;
    public int MinTime => minTime;

    private void Start()
    {
        roundTime = Balance.instance.FirstRoundTime;
    }

    public void IncreaseRoundTime()
    {
        roundTime += Balance.instance.IncrementRoundTime;
        TimerUpdate();
    }

    public void ChangeTimer(float levelStartTime)
    {
        leftTime = levelStartTime + roundTime - Time.time;
        TimerUpdate();
        Debug.Log("Start timer");
    }

    public void TimerUpdate()
    {
        TimeChanged?.Invoke();
    }
}
