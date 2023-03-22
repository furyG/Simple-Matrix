using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public static Balance instance;

    [SerializeField] private float firstRoundTime;
    [SerializeField] private float incrementRoundTime;

    [SerializeField] private int firstRoundPointsCap;
    [SerializeField] private int incrementPointsCap;

    [SerializeField] private float spawnNumbersTime;
    [SerializeField] private float numbersRunTime;

    [SerializeField] private int startTapesAmount;
    [SerializeField] private float bonusSpawnChance;
    [SerializeField] private int pointsForNumber;

    [SerializeField] private Vector2 floatingPointsEndPosition;

    public float FirstRoundTime => firstRoundTime;
    public float IncrementRoundTime => incrementRoundTime;

    public int FirstRoundPointsCap => firstRoundPointsCap;
    public int IncrementPointsCap => incrementPointsCap;

    public float SpawnNumbersTime => spawnNumbersTime;
    public float NumbersRunTime => numbersRunTime;

    public int StartTapesAmount => startTapesAmount;
    public float BonusSpawnChance => bonusSpawnChance;
    public int PointsForNumber => pointsForNumber;

    public Vector2 FloatingPointsEndPosition => floatingPointsEndPosition;

    private void Awake()
    {
        instance = this;
    }
}
