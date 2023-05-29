using Architecture;
using Tapes;

public class PlayState : IState
{
    private GameModeManager main;

    private TapeSpawner _tapeSpawner;
    private PointsInteractor _pointsInteractor;
    private LifesInteractor _lifesInteractor;
    private TimerInteractor _timerInteractor;

    public PlayState(GameModeManager main)
    {
        this.main = main;

        this._tapeSpawner = main.tapeSpawner;
        this._lifesInteractor = Game.GetInteractor<LifesInteractor>();
        this._timerInteractor = Game.GetInteractor<TimerInteractor>();
        this._pointsInteractor = Game.GetInteractor<PointsInteractor>();
    }
    public void Enter(ButtonType fromButton)
    {
        switch(fromButton)
        {
            case ButtonType.Start:
                _tapeSpawner.SpawnTapes(true);
                _pointsInteractor.ResetPoints();
                _timerInteractor.StartRoundTimer();
                _lifesInteractor.ResetLifes();
                break;
            case ButtonType.Continue:
                _tapeSpawner.SpawnTapes(false);
                _lifesInteractor.RemoveHeartScore(this, 1);
                _timerInteractor.StartRoundTimer();
                break;
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {
        _tapeSpawner.StopTapes();
        _tapeSpawner.ClearTapes();
    }
}
