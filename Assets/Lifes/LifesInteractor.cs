using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LifesInteractor : Interactor, IBonusReciever
{
    public event Action<float> heartScoreChanged;
    public event Action<List<Vector2>> comboRecieved;

    public float heartScore => repository.heartsScore;

    private PointsInteractor _pointsInteractor;
    private LifesRepository repository;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<LifesRepository>();
        _pointsInteractor = Game.GetInteractor<PointsInteractor>();
    }
    public override void OnStart()
    {
        repository.heartsScore = repository.startHeartScoreAmount;
        heartScoreChanged?.Invoke(heartScore);

        if(_pointsInteractor != null)
        {
            _pointsInteractor.comboRecieved += OnComboRecieved;
        }
    }
    public void AddHeartScore(object sender, float amount)
    {
        repository.heartsScore += amount;
        repository.heartsScore = Mathf.Clamp(repository.heartsScore, 0, repository.maxHeartScore);

        heartScoreChanged?.Invoke(heartScore);
    }
    public void RemoveHeartScore(object sender, float amount)
    {
        repository.heartsScore -= amount;
        repository.heartsScore = Mathf.Clamp(repository.heartsScore, 0, repository.maxHeartScore);

        heartScoreChanged?.Invoke(heartScore);
    }
    public bool IsEnoughLifes(int compare) => heartScore >= compare;

    public void ResetLifes()
    {
        repository.heartsScore = repository.startHeartScoreAmount;

        heartScoreChanged?.Invoke(heartScore);
    }

    public void TakeBonus()
    {
        AddHeartScore(null, 1);
    }
    private void OnComboRecieved(List<Vector2> nums)
    {
        AddHeartScore(null, nums.Count * repository.heartPointsForOnePoint);
        comboRecieved?.Invoke(nums);
    }
}
