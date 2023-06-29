using Tapes;
using UnityEngine;

public class GameModeManager : Singleton<GameModeManager>
{
    public Board board => _board;
    [SerializeField] private Board _board;

    public PlayerManager playerManager => _playerManager;
    private PlayerManager _playerManager;

    public GameUISwitcher UISwitcher => _UISwitcher;
    [SerializeField] private GameUISwitcher _UISwitcher;

    private StateMachine mainStateMachine;

    public void StartGameFromMainMenu()
    {
        _playerManager = GetComponent<PlayerManager>();

        mainStateMachine = new StateMachine(this);

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
