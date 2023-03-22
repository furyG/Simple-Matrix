using System;
using Architecture;

public class LevelInteractor : Interactor
{
    public event Action levelChangedEvent;

    public int currentLevel => repository.level;

    private LevelRepository repository;

    public override void OnCreate()
    {
        repository = Game.GetRepository<LevelRepository>();
    }
    public override void Initialize()
    {
        levelChangedEvent?.Invoke();
    }

    public void NextLevel()
    {
        this.repository.level++;

        UpdateLevel();
    }
    public void NewGame()
    {
        this.repository.level = 1;

        UpdateLevel();
    }
    public void UpdateLevel()
    {
        levelChangedEvent?.Invoke();
    }
}
