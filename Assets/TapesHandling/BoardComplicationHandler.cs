using Architecture;
using Tapes;
using UnityEngine;

public class BoardComplicationHandler
{
    public int tapesAmount
    {
        get => _tapesAmount;
        private set
        {
            _tapesAmount = value;
            _tapesAmount = Mathf.Clamp(value, _tapeSettingsConfig.startTapesAmount, 5);
        }
    }
    private int _tapesAmount;

    private int currentPointsAmount;
    private readonly Board _board;
    private readonly PointsInteractor _pointsInteractor;
    private readonly TapeSettingsConfig _tapeSettingsConfig;
    private readonly TilesBlockHandler _tilesBlockHandler;


    public BoardComplicationHandler(Board board, TilesBlockHandler tilesBlockHandler)
    {
        _board = board;

        _pointsInteractor = Game.GetInteractor<PointsInteractor>();
        _tapeSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<TapeSettingsConfig>();

        _tilesBlockHandler = tilesBlockHandler;
        tapesAmount = _tapeSettingsConfig.startTapesAmount;

        if (_pointsInteractor != null)
        {
            _pointsInteractor.pointsChanged += OnPointsChangedEvent;
        }
    }

    public void OnDestroy()
    {
        if (_pointsInteractor != null)
        {
            _pointsInteractor.pointsChanged -= OnPointsChangedEvent;
        }
    }
    public void OnNewGame()
    {
        tapesAmount = _tapeSettingsConfig.startTapesAmount;
    }

    private void OnPointsChangedEvent(int points)
    {
        currentPointsAmount = points;

        CheckForNewTapes();

        _tilesBlockHandler.CheckForTileBlock(currentPointsAmount);
    }
    private void CheckForNewTapes()
    {
        if (_tapesAmount < _tapeSettingsConfig.startTapesAmount + 2)
        {
            if (currentPointsAmount >= _tapeSettingsConfig.pointsForFirstNewTape && _tapesAmount < _tapeSettingsConfig.startTapesAmount + 1)
            {
                tapesAmount++;
                _board.SpawnTape().StartContentSpawning();
                _board.InitializeTileNeighbours();
            }
            if (currentPointsAmount >= _tapeSettingsConfig.pointsForSecondNewTape)
            {
                tapesAmount += 2;
                _board.SpawnTape().StartContentSpawning();
                _board.InitializeTileNeighbours();
            }
        }
    }
}
