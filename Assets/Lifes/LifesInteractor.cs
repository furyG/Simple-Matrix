using Architecture;
using System;
using System.Diagnostics;

public class LifesInteractor : Interactor, IBonusReciever
{
    public event Action<int> lifesAmountChanged;

    public int lifes => repository.lifes;
    public int maximumLifes => repository.maximumLifes;

    private LifesRepository repository;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<LifesRepository>();
        lifesAmountChanged?.Invoke(lifes);
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

    public void TakeBonus()
    {
        AddLife(null, 1);
    }
}
