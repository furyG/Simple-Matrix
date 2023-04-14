using System.Collections;
using System.Collections.Generic;
using Tapes;
using UnityEngine;
using Architecture;

public class TapesHandler
{
    private List<List<Neighbour>> neighbours;

    private TapeSpawner _spawner;
    private PointsInteractor _pointsInteractor;

    public TapesHandler(TapeSpawner spawner)
    {
        this._spawner = spawner;
        this._pointsInteractor = Game.GetInteractor<PointsInteractor>();
    }
    private void OnTapeClicked(TapeManager tapeManager, List<NumberManager> sortedChildList)
    {
        neighbours = new List<List<Neighbour>>();

        foreach (var tape in _spawner.tapes)
        {
            var managers = tape.CompareChildLists(sortedChildList);
            var localNeighbours = managers.ConvertAll(x => x.neighbour);
            neighbours.Add(localNeighbours);
        }
        UpdateNeighboursForChildren();

        var comboList = tapeManager.lastNumber.neighbour.TryGetCombo();

        if (comboList != null)
        {
            _pointsInteractor.RecieveCombo(comboList.ConvertAll(x => x.position));
        }
    }
    private void UpdateNeighboursForChildren()
    {
        for (int y = 0; y < neighbours.Count; y++)
        {
            for (int x = 0; x < neighbours[y].Count; x++)
            {
                neighbours[y][x].UpdateNeighbours(neighbours);
            }
        }
    }
    public void Subscribe(TapeManager tape)
    {
        tape.OnChildSort += OnTapeClicked;
    }
    public void Unsubscribe(TapeManager tape)
    {
        tape.OnChildSort -= OnTapeClicked;
    }
}
