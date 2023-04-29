using System;
using Architecture;

public class LevelInteractor : Interactor
{
    public event Action<int> levelChanged;

    public int currentLevel => repository.level;

    private LevelRepository repository;
    private PointsInteractor pointsInteractor;
    
    public override void OnCreate()
    {
        repository = Game.GetRepository<LevelRepository>();
    }
    public override void Initialize()
    {
        levelChanged?.Invoke(currentLevel);

        if (pointsInteractor!=null)
        {
            pointsInteractor.pointsForLevelUpCollected += NextLevel;
        }
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
        levelChanged?.Invoke(currentLevel);
    }
}
