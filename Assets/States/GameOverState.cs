using Architecture;

public class GameOverState : IState
{
    private GameModeManager main;
    private TimerInteractor _timerInteractor;
    public GameOverState(GameModeManager main)
    {
        this.main = main;
        _timerInteractor = Game.GetInteractor<TimerInteractor>();
    }
    public void Enter()
    {
        _timerInteractor.Stop();
    }

    public void Update()
    {
        
    }

    public void Exit()
    {

    }
}
