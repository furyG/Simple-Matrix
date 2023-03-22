using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using Tapes;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Numberable : Tile
{
    public bool boarded = false;
    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            NumberName = _number.ToString();

            Sprite[] s = Resources.LoadAll<Sprite>("Sprites/numbers");
            rend.sprite = s[_number];
        }
    }
    private int _number;
    public string NumberName
    {
        get => _number.ToString();
        set
        {
            _numberName = value;
            name = _numberName;
        }
    }
    private string _numberName;

    protected TapesInteractor tapesInteractor { get; set; }

    protected int x { get; set; }
    protected int y { get; set; }

    public Numberable Left { get; private set; }
    public Numberable Top { get; private set; }
    public Numberable Right { get; private set; }
    public Numberable Bottom { get; private set; }

    public Numberable[] Neighbors => new[]
    {
        Left,
        Top,
        Right,
        Bottom
    };

    protected override void Awake()
    {
        base.Awake();

        tapesInteractor = Game.GetInteractor<TapesInteractor>();
        if (tapesInteractor != null)
        {
            tapesInteractor.tapesChanged += OnTapesChangedEvent;
        }
    }
    protected void OnDestroy()
    {
        if (tapesInteractor != null)
        {
            tapesInteractor.tapesChanged -= OnTapesChangedEvent;
        }
    }

    private void OnTapesChangedEvent(List<List<Numberable>> numMatrix)
    {
        UpdateNeighbours(numMatrix);
    }

    private void UpdateNeighbours(List<List<Numberable>> numMatrix)
    {
        Vector2Int tileCoordinates = GetThisTileCoordinates(numMatrix);
        this.x = tileCoordinates.x;
        this.y = tileCoordinates.y;

        Left = x > 0 ? numMatrix[y][x - 1] : null;
        Top = y > 0 ? numMatrix[y - 1][x] : null;
        Right = x < numMatrix[y].Count - 1 ? numMatrix[y][x + 1] : null;
        Bottom = y < numMatrix.Count - 1 ? numMatrix[y + 1][x] : null;
    }

    private List<Numberable> GetConnectedTiles(List<Numberable> exclude = null)
    {
        var result = new List<Numberable> { this, };

        if (exclude == null)
        {
            exclude = new List<Numberable> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (var neighbour in Neighbors)
        {
            if (neighbour == null || exclude.Contains(neighbour) || !IsCorrectNumber(neighbour) || neighbour.Number == 0) continue;
            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        return result;
    }

    private bool IsCorrectNumber(Numberable neighbour) => Mathf.Abs(neighbour.Number - this.Number) == 1;

    private Vector2Int GetThisTileCoordinates(List<List<Numberable>> numberables)
    {
        Numberable element = this;

        for(int y = 0; y < numberables.Count; y++)
        {
            for(int x = 0; x < numberables[y].Count; x++)
            {
                if (numberables[y][x].Equals(element))
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return Vector2Int.zero;
    }
    public List<Numberable> TryGetCombo()
    {
        List<Numberable> combo = GetConnectedTiles();
        if(combo.Count >= 3)
        {
            foreach(var numberable in combo)
            {
                numberable.Number = 0;
            }
            return combo;
        }
        return null;
    }
}

