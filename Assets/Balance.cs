using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public static Balance instance;

    public float FirstRoundTime;
    public float IncrementRoundTime;

    public float FirstRoundPointsCap;
    public float IncrementPointsCap;

    public float SpawnNumbersTime;
    public float NumbersRunTime;

    public int StartTapesAmount;

    private void Awake()
    {
        instance = this;
    }
}
