using Architecture;
using Tapes;

public class PlayState : IState
{
    private Board _board;
    private PointsInteractor _pointsInteractor;
    private LifesInteractor _lifesInteractor;
    private TimerInteractor _timerInteractor;

    public PlayState(GameModeManager main)
    {
        this._board = main.board;
        this._lifesInteractor = Game.GetInteractor<LifesInteractor>();
        this._timerInteractor = Game.GetInteractor<TimerInteractor>();
        this._pointsInteractor = Game.GetInteractor<PointsInteractor>();
    }
    public void Enter(ButtonType fromButton)
    {
        switch (fromButton)
        {
            case ButtonType.Start:
                _board.OnNewGame();
                _board.StartTapesContentSpawning();
                _pointsInteractor.ResetPoints();
                _lifesInteractor.ResetLifes();
                break;
            case ButtonType.Continue:
                _lifesInteractor.RemoveHeartScore(this, 1);
                _board.StartTapesContentSpawning();
                break;
        }
        _timerInteractor.StartRoundTimer();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        _board.ClearTapesSpawn();
        _timerInteractor.Stop();
    }
}
