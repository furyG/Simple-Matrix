using Architecture;
using Tapes;
using UnityEngine;

public class TapeComplicationHandler
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
    private readonly TapeSpawner _tapeSpawner;
    private readonly PointsInteractor _pointsInteractor;
    private readonly TapeSettingsConfig _tapeSettingsConfig;
    private readonly TilesBlockHandler _tilesBlockHandler;


    public TapeComplicationHandler(TapeSpawner tapeSpawner, TilesBlockHandler tilesBlockHandler)
    {
        _tapeSpawner = tapeSpawner;

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
                _tapeSpawner.SpawnTape();
                _tapeSpawner.InitializeTileNeighbours();
            }
            if (currentPointsAmount >= _tapeSettingsConfig.pointsForSecondNewTape)
            {
                tapesAmount += 2;
                _tapeSpawner.SpawnTape();
                _tapeSpawner.InitializeTileNeighbours();
            }
        }
    }
}
