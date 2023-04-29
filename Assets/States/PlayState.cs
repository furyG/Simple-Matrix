using Architecture;
using Tapes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayState : IState
{
    private GameModeManager main;
    private TapeSpawner _tapeSpawner;
    private LevelInteractor _levelInteractor;

    public PlayState(GameModeManager main)
    {
        this.main = main;

        this._tapeSpawner = main.tapeSpawner;
        this._levelInteractor = Game.GetInteractor<LevelInteractor>();
    }
    public void Enter(ButtonType fromButton)
    {
        switch(fromButton)
        {
            case ButtonType.Start:
                _tapeSpawner.SpawnTapes();
                _levelInteractor.NewGame();
                Points.Reset();
                Timer.StartRoundTimer();
                break;
            case ButtonType.Continue:
                _levelInteractor.NewGame();
                _tapeSpawner.SpawnTapes();
                Lifes.RemoveLife(this, 1);
                break;
        }
    }

    public void Update()
    {

    }

    public void Exit(ButtonType fromButton)
    {
        _tapeSpawner.StopTapes();
    }
}
