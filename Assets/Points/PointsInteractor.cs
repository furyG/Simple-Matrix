using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PointsInteractor : Interactor
{
    public event Action<int> pointsChanged;
    public event Action<List<Vector2>> comboRecieved;

    public int points => repository.points;

    private int pointsForNumber => repository.pointsForNumber;
    private PointsRepository repository;

    private AudioInteractor _audioInteractor;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<PointsRepository>();

        _audioInteractor = Game.GetInteractor<AudioInteractor>();
    }
    public override void Initialize()
    {
        ResetPoints();
        pointsChanged?.Invoke(points);
    }

    public void RecieveCombo(List<Vector2> numsToAnimate)
    {
        AddPoints(null, numsToAnimate.Count * pointsForNumber);
        comboRecieved?.Invoke(numsToAnimate);

        _audioInteractor?.PlayComboBuildedSound();
    }
    public void AddPoints(object sender, int value)
    {
        this.repository.points += value;
        pointsChanged?.Invoke(points);
    }
    public void LosePoints(object sender, int value)
    {
        this.repository.points -= value;
        pointsChanged?.Invoke(points);
    }
    public void ResetPoints()
    {
        this.repository.points = 0;
        pointsChanged?.Invoke(points);
    }
}

