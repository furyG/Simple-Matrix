using System;
using System.Collections.Generic;
using Architecture;
using UnityEngine;

public class PointsInteractor : Interactor
{
    public event Action pointsChanged;
    public event Action<List<Vector2>> comboRecieved;
    public event Action pointsForLevelUpCollected;

    public int points => repository.points;
    public int maxPointsOnLvl => repository.pointsForNextLevel;

    private int pointsForNumber;
    private PointsRepository repository;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<PointsRepository>();
        Points.Initialize(this);
    }
    public override void Initialize()
    {
        pointsChanged?.Invoke();

        pointsForNumber = Balance.GetInstance().PointsForNumber;
    }

    private void UpdateRoundPoints()
    {
        this.repository.pointsForNextLevel += repository.pointsIncrement + points/2;
        pointsChanged?.Invoke();
    }

    public bool IsEnoughPoints() => points >= maxPointsOnLvl;

    public void RecieveCombo(List<Vector2> numsToAnimate)
    {
        comboRecieved?.Invoke(numsToAnimate);
        AddPoints(null, numsToAnimate.Count * pointsForNumber);
    }
    public void AddPoints(object sender, int value)
    {
        this.repository.points += value;
        pointsChanged?.Invoke();

        if (IsEnoughPoints())
        {
            pointsForLevelUpCollected?.Invoke();
            UpdateRoundPoints();
        }
    }
    public void LosePoints(object sender, int value)
    {
        this.repository.points -= value;
        pointsChanged?.Invoke();
    }
    public void Reset()
    {
        this.repository.points = 0;
        pointsChanged?.Invoke();
    }
}

