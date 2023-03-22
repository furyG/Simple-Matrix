using System;
using System.Collections.Generic;
using Architecture;
using UnityEngine;

public class PointsInteractor : Interactor
{
    public event Action OnPointsChanged;
    public event Action<List<Numberable>> OnComboRecieved;

    public int points => repository.points;
    public int maxPointsOnLvl => repository.pointsForNextLevel;

    public int combo => repository.combo;

    private PointsRepository repository;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<PointsRepository>();
    }
    public override void Initialize()
    {
        OnPointsChanged?.Invoke();
    }

    public bool IsEnoughPoints() => points >= maxPointsOnLvl;

    public void RecieveCombo(List<Numberable> numsToAnimate)
    {
        foreach(var num in numsToAnimate)
        {
            Debug.Log("Combo with: " + num.Number);
        }
        OnComboRecieved?.Invoke(numsToAnimate);
    }
    public void AddPoints(object sender, int value)
    {
        this.repository.points += value;
        OnPointsChanged?.Invoke();
    }
    public void LosePoints(object sender, int value)
    {
        this.repository.points -= value;
        OnPointsChanged?.Invoke();
    }
    public void Reset()
    {
        this.repository.points = 0;
        OnPointsChanged?.Invoke();
    }
    public void FPCallback(FloatingPoints floatingPoints)
    {
        AddPoints(floatingPoints, floatingPoints.points);
    }
}

