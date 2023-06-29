using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TapeNumberSetter
{
    private const int tileThreshold = 119;

    private List<TileNeighbour> _tileNeighbours;
    private PointsInteractor _pointsInteractor;

    public TapeNumberSetter(List<TileNeighbour> tileNeighbours)
    {
        _tileNeighbours = tileNeighbours;
        _pointsInteractor = Game.GetInteractor<PointsInteractor>();
    }

    public void SetNumber(NumberManager lastNumber)
    {
        float buffer = tileThreshold / 2 + 5;

        TileNeighbour closest = GetClosestTile((int)lastNumber.transform.localPosition.x);
        float delta = Mathf.Abs(closest.localPos.x - lastNumber.transform.localPosition.x);
        if (delta < buffer)
        {
            ReplaceNumberInTile(closest, lastNumber);
        }
        else
        {
            lastNumber.gameObject.SetActive(false);
        }
    }

    public void ClearAllNeighbours()
    {
        foreach(var neighbour in _tileNeighbours)
        {
            neighbour.ClearTileNumber();
        }
    }

    private void ReplaceNumberInTile(TileNeighbour closest, NumberManager lastNumber)
    {
        closest.ChangeNumberValue(lastNumber.number);
        lastNumber.gameObject.SetActive(false);

        TryGetCombo(closest);
    }

    private void TryGetCombo(TileNeighbour closest)
    {
        var comboList = closest.TryGetCombo();

        if (comboList != null)
        {
            _pointsInteractor.RecieveCombo(comboList.ConvertAll(x => x.pos));
        }
    }

    private TileNeighbour GetClosestTile(int numXPos)
    {
        TileNeighbour closest = _tileNeighbours[0];

        for (int i = 0; i < _tileNeighbours.Count; i++)
        {
            if (Mathf.Abs(numXPos - _tileNeighbours[i].localPos.x) < Mathf.Abs(numXPos - closest.localPos.x))
            {
                closest = _tileNeighbours[i];
            }
        }
        return closest;
    }
}
