using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbour 
{
    public int number => manager.number;
    public Vector2 position => manager.transform.position;

    public Neighbour Left { get; private set; }
    public Neighbour Top { get; private set; }
    public Neighbour Right { get; private set; }
    public Neighbour Bottom { get; private set; }

    private int x { get; set; }
    private int y { get; set; }

    private readonly NumberManager manager;

    public Neighbour[] Neighbors => new[]
    {
        Left,
        Top,
        Right,
        Bottom
    };
    public Neighbour(NumberManager manager) => this.manager = manager;

    private List<Neighbour> GetConnectedTiles(List<Neighbour> exclude = null)
    {
        var result = new List<Neighbour> { this, };

        if (exclude == null)
        {
            exclude = new List<Neighbour> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (var neighbour in Neighbors)
        {
            if (neighbour == null || exclude.Contains(neighbour) || !IsCorrectNumber(neighbour) || neighbour.number == 0) continue;
            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        return result;
    }

    private bool IsCorrectNumber(Neighbour neighbour)
    {
        bool match = Mathf.Abs(neighbour.number - this.number) == 1;
        return match;
    }

    private Vector2Int GetThisTileCoordinates(List<List<Neighbour>> neighboursMatrix)
    {
        Neighbour element = this;

        for (int y = 0; y < neighboursMatrix.Count; y++)
        {
            for (int x = 0; x < neighboursMatrix[y].Count; x++)
            {
                if (neighboursMatrix[y][x].Equals(element))
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return Vector2Int.zero;
    }
    public void UpdateNeighbours(List<List<Neighbour>> numMatrix)
    {
        Vector2Int tileCoordinates = GetThisTileCoordinates(numMatrix);
        this.x = tileCoordinates.x;
        this.y = tileCoordinates.y;

        Left = x > 0 ? numMatrix[y][x - 1] : null;
        Top = y > 0 ? numMatrix[y - 1][x] : null;
        Right = x < numMatrix[y].Count - 1 ? numMatrix[y][x + 1] : null;
        Bottom = y < numMatrix.Count - 1 ? numMatrix[y + 1][x] : null;
    }
    public void ChangeNumberValue(int number) => manager.SetNumberValues(number);
    
    public List<Neighbour> TryGetCombo()
    {
        List<Neighbour> combo = GetConnectedTiles();
        if (combo.Count >= 3)
        {
            foreach (var neighbour in combo)
            {
                neighbour.ChangeNumberValue(0);
            }
            return combo;
        }
        return null;
    }
}
