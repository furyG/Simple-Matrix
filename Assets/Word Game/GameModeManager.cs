using Architecture;
using Tapes;
using UnityEngine;

public class GameModeManager : Singleton<GameModeManager>
{
    public TapeSpawner tapeSpawner => _tapeSpawner;
    [SerializeField] private TapeSpawner _tapeSpawner;

    public PlayerManager playerManager => _playerManager;
    private PlayerManager _playerManager;

    public GameUISwitcher UISwitcher => _UISwitcher;
    [SerializeField] private GameUISwitcher _UISwitcher;

    private StateMachine mainStateMachine;

    public override void Awake()
    {
        base.Awake();

        Game.Run();

        _playerManager = GetComponent<PlayerManager>();
        mainStateMachine = new StateMachine(this);
    }

    private void Start()
    {
        mainStateMachine.Initialize(mainStateMachine.mainMenuState);

        ToMainMenu();
        StartCoroutine(UISwitcher.SwitchCanvasRoutine(CanvasType.mainMenu));
    }

    public void StartNewGame(ButtonType fromButton)
    {
        mainStateMachine.TransitionTo(mainStateMachine.playState, fromButton);
    }
    public void ContinueGame(ButtonType fromButton)
    {
        mainStateMachine.TransitionTo(mainStateMachine.playState, fromButton);
    }
    public void ToMainMenu()
    {
        mainStateMachine.TransitionTo(mainStateMachine.mainMenuState);
    }
    public void GameOver(bool fromTimer)
    {
        if (fromTimer)
        {
            Time.timeScale = 0;
            UISwitcher.SwitchPopup(PopupType.gameOver);
        }

        mainStateMachine.TransitionTo(mainStateMachine.gameOverState);
    }

    private void Update()
    {
        mainStateMachine.Update();
    }
}
