using Architecture;
using System.Collections;
using Tapes;
using UnityEngine;

public class GameModeManager : Singleton<GameModeManager>
{
    public TapeSpawner tapeSpawner => _tapeSpawner;
    [SerializeField] private TapeSpawner _tapeSpawner;

    public StateMachine MainStateMachine => mainStateMachine;

    private StateMachine mainStateMachine;

    public override void Awake()
    {
        base.Awake();

        Game.Run();

        mainStateMachine = new StateMachine(this);
        mainStateMachine.Initialize(mainStateMachine.mainMenuState);    
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
    public void GameOver(ButtonType fromButton = default)
    {
        if(fromButton == default)
        {
            CanvasManager.GetInstance().SwitchPopup(PopupType.GameOver);
            Time.timeScale = 0;
        }
        mainStateMachine.TransitionTo(mainStateMachine.gameOverState);
    }

    private void Update()
    {
        mainStateMachine.Update();
    }
}
