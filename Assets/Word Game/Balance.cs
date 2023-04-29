using System;
using UnityEngine;

public class Balance : Singleton<Balance>, IBonusReciever
{
    public event Action slowBonusTaken;
    public event Action slowBonusDurationEnd;

    [SerializeField] private float firstRoundTime;
    [SerializeField] private float timerBonusIncrementAmount;
    [SerializeField] private float roundUpTimerIncrement;

    [SerializeField] private int firstRoundPointsCap;
    [SerializeField] private int incrementPointsCap;

    [SerializeField] private float spawnNumbersTime;
    [SerializeField] private float numberRunDuration;
    [SerializeField] private float slowNumberRunDuration;

    [SerializeField] private int startTapesAmount;
    [SerializeField] private float bonusSpawnChance;
    [SerializeField] private int pointsForNumber;

    [SerializeField] private int tilesOnTape;
    [SerializeField] private int tapeActiveZoneDistance;

    [SerializeField] private float pointsFlyingTime;
    [SerializeField] private int maximumLifes;

    [SerializeField] private float bonusTimesAdd;
    [SerializeField] private float slowBonusDuration;

    private bool _slowed = false;

    public float FirstLevelTime => firstRoundTime;
    public float TimerBonusIncrementAmount => timerBonusIncrementAmount;

    public float LevelUpTimerIncrement => roundUpTimerIncrement;

    public int FirstRoundPointsCap => firstRoundPointsCap;
    public int IncrementPointsCap => incrementPointsCap;

    public float SpawnNumbersTime => spawnNumbersTime;
    public float NumberRunDuration => numberRunDuration;
    public float SlowNumberRunDuration => slowNumberRunDuration;
    public bool Slowed => _slowed;

    public int StartTapesAmount => startTapesAmount;
    public float BonusSpawnChance => bonusSpawnChance;
    public int TileOnTape => tilesOnTape;
    public int TapeActiveZoneDistance => tapeActiveZoneDistance;
    public int PointsForNumber => pointsForNumber;
    public float PointsFlyingTime => pointsFlyingTime;

    public int MaximumLifes => maximumLifes;    
    public void TakeBonus()
    {
        slowBonusTaken?.Invoke();
        Invoke(nameof(EndSlowBonusEffect), slowBonusDuration);
        _slowed = true;
    }
    private void EndSlowBonusEffect()
    {
        slowBonusDurationEnd?.Invoke();
        _slowed = false;
    }
}
