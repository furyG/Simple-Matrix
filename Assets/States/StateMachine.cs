using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class StateMachine 
{
    public event Action <IState> onStateChanged;
    public IState CurrentState { get; private set; }

    public PlayState playState;
    public GameOverState gameOverState;
    public MainMenuState mainMenuState;

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }
    public async void TransitionTo(IState nextState, ButtonType fromButton)
    {
        await Task.Yield();

        CurrentState.Exit(fromButton);
        CurrentState = nextState;
        nextState.Enter(fromButton);

        onStateChanged?.Invoke(CurrentState);
        Debug.Log("Transition to: "+nextState.ToString());
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

        onStateChanged?.Invoke(CurrentState);
        Debug.Log("Transition to: " + nextState.ToString());
    }
    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
    public StateMachine(GameModeManager main)
    {
        this.playState = new PlayState(main);
        this.gameOverState = new GameOverState(main);
        this.mainMenuState = new MainMenuState(main);
    }

}
