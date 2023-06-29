using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBlockHandler
{
    public List<List<TileNeighbour>> tileNeighbours { get; set; }

    private int blockedTilesAmount
    {
        get => _blockedTilesAmount;
        set
        {
            _blockedTilesAmount = value;
            _blockedTilesAmount = Mathf.Clamp(value, 0, _tapeSettingsConfig.maxBlockersAmount);
        }
    }
    private int _blockedTilesAmount;

    private int _currentPointsAmount;
    private readonly TapeSettingsConfig _tapeSettingsConfig;
    private readonly List<TileNeighbour> _blockedTilesList;

    public TilesBlockHandler()
    {
        _blockedTilesList = new List<TileNeighbour>();

        _tapeSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<TapeSettingsConfig>();

        Coroutines.StartRoutine(ChangeBlockersPosition());
    }

    public void OnTapesClear()
    {
        Coroutines.StopRoutine(ChangeBlockersPosition());
        ClearBlockers();
    }

    public void CheckForTileBlock(int currentPointsAmount)
    {
        _currentPointsAmount = currentPointsAmount;
        SpawnBlockers(currentPointsAmount);
    }

    private void ClearBlockers()
    {
        if (_blockedTilesList != null)
        {
            if (_blockedTilesList.Count > 0)
            {
                foreach (var tile in _blockedTilesList)
                {
                    tile?.Unblock();
                }
                _blockedTilesList.Clear();
            }
        }
    }
    private void SpawnBlockers(int currentPointsAmount)
    {
        blockedTilesAmount = Mathf.FloorToInt(currentPointsAmount / _tapeSettingsConfig.pointsForBlocker);
        blockedTilesAmount -= _blockedTilesList.Count;

        for (int i = 0; i < blockedTilesAmount; i++)
        {
            int y = Random.Range(0, tileNeighbours.Count);
            int x = Random.Range(0, tileNeighbours[0].Count);

            TileNeighbour randomTile = tileNeighbours[y][x];
            randomTile.Block();

            _blockedTilesList.Add(randomTile);
        }
    }
    private IEnumerator ChangeBlockersPosition()
    {
        yield return new WaitForSeconds(_tapeSettingsConfig.blockersLifeTime);

        ClearBlockers();

        SpawnBlockers(_currentPointsAmount);

        Coroutines.StartRoutine(ChangeBlockersPosition());
    }
}
