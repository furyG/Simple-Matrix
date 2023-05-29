using System.Collections.Generic;
using UnityEngine;

public class TileNeighbour
{
    public int number { get; private set; }

    public Vector2 localPos => _tile.transform.localPosition;
    public Vector2 pos => _tile.transform.position;
    public TileNeighbour Left { get; private set; }
    public TileNeighbour Top { get; private set; }
    public TileNeighbour Right { get; private set; }
    public TileNeighbour Bottom { get; private set; }

    public int x { get; set; }
    public int y { get; set; }

    private bool _isBlocked = false;
    private readonly Tile _tile;

    public TileNeighbour[] Neighbors => new[]
    {
        Left,
        Top,
        Right,
        Bottom
    };
    public TileNeighbour(Tile tile)
    {
        this._tile = tile;
    }

    public void InitializeTileNeighbour(int x, int y, List<List<TileNeighbour>> tilesMatrix)
    {
        this.x = x;
        this.y = y;

        Left = x > 0 ? tilesMatrix[y][x - 1] : null;
        Top = y > 0 ? tilesMatrix[y - 1][x] : null;
        Right = x < tilesMatrix[y].Count - 1 ? tilesMatrix[y][x + 1] : null;
        Bottom = y < tilesMatrix.Count - 1 ? tilesMatrix[y + 1][x] : null;
    }

    private List<TileNeighbour> GetConnectedTiles(List<TileNeighbour> exclude = null)
    {
        var result = new List<TileNeighbour> { this, };

        if (exclude == null)
        {
            exclude = new List<TileNeighbour> { this, };
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

    private bool IsCorrectNumber(TileNeighbour neighbour)
    {
        bool match = Mathf.Abs(neighbour.number - this.number) == 1;
        return match;
    }

    public void ChangeNumberValue(int number)
    {
        if (_isBlocked) return;

        this.number = number;
        _tile.SetNumberRender(number);
    }

    public void ClearTileNumber()
    {
        this.number = 0;
        _tile.ClearNumberRender();
    }
    public void PlayTakingAnimation()
    {
        _tile.PlayTakingAnimation();
    }

    public List<TileNeighbour> TryGetCombo()
    {
        List<TileNeighbour> combo = GetConnectedTiles();
        if (combo.Count >= 3)
        {
            foreach (var neighbour in combo)
            {
                neighbour.PlayTakingAnimation();
            }
            return combo;
        }
        return null;
    }
    public void Block()
    {
        _isBlocked = true;
        _tile.BlockTile();
        ClearTileNumber();
    }
    public void Unblock()
    {
        _isBlocked = false;
        _tile.UnblockTile();
    }
}
