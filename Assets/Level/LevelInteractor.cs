using System;
using Architecture;

public class LevelInteractor : Interactor
{
    public event Action<int> levelChanged;

    public int currentLevel => repository.level;

    private ButtonsInteractor buttonsInteractor;
    private ContinueButton continueButton;
    private StartButton startButton;

    private LevelRepository repository;
    

    public override void OnCreate()
    {
        repository = Game.GetRepository<LevelRepository>();

        buttonsInteractor = Game.GetInteractor<ButtonsInteractor>();
    }
    public override void Initialize()
    {
        levelChanged?.Invoke(currentLevel);

        continueButton = buttonsInteractor.GetButton<ContinueButton>();
        startButton = buttonsInteractor.GetButton<StartButton>();

        if (continueButton != null)
        {
            continueButton.buttonClicked += NextLevel;
        }
        if (startButton != null)
        {
            startButton.buttonClicked += NewGame;
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
