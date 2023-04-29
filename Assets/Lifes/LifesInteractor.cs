using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LifesInteractor : Interactor, IBonusReciever
{
    public event Action<int> lifesAmountChanged;

    public int lifes => repository.lifes;

    private LifesRepository repository;
    private PointsInteractor pointsInteractor;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<LifesRepository>();

        pointsInteractor = Game.GetInteractor<PointsInteractor>();

        lifesAmountChanged?.Invoke(lifes);
        Lifes.Initialize(this);
    }
    public override void Initialize()
    {
        if(pointsInteractor!= null)
        {
            pointsInteractor.comboRecieved += OnComboRecieved;
        }
    }
    private void OnComboRecieved(List<Vector2> nums)
    {
        if(nums.Count > 4) 
        {
            AddLife(null, 1);
        }
    }
    public void AddLife(object sender, int amount)
    {
        repository.lifes += amount;
        repository.lifes = Math.Clamp(repository.lifes, 0, repository.maximumLifes);

        lifesAmountChanged?.Invoke(lifes);
    }
    public void RemoveLife(object sender, int amount)
    {
        repository.lifes -= amount;
        repository.lifes = Math.Clamp(repository.lifes, 0, repository.maximumLifes);

        lifesAmountChanged?.Invoke(lifes);
    }
    public bool IsEnoughLifes() => lifes >= 1;

    public void ResetLifes()
    {
        repository.lifes = 0;

        lifesAmountChanged?.Invoke(lifes);
    }

    public void TakeBonus()
    {
        AddLife(null, 1);
    }
}
