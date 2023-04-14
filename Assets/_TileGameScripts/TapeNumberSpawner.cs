using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TapeNumberSpawner : MonoBehaviour
{
    private List<TileNew> tiles;
    private Button tapeButton;
    private TileNew activeTile;

    private void Awake()
    {
        tapeButton = GetComponent<Button>();
        tapeButton.onClick.AddListener(OnTapeClick);

        tiles = GetComponentsInChildren<TileNew>().ToList();
    }
    private void Start()
    {
        Invoke(nameof(SpawnNumber), 1);
    }
    private void SpawnNumber()
    {
        int position = Random.Range(0, 2);
        position = position == 0 ? 0 : tiles.Count - 1;

        int value = Random.Range(0, 6);

        activeTile = tiles[position].SetNumber(value);

        Invoke(nameof(MoveNumber), 0.5f);
    }

    private void MoveNumber()
    {
        int index = tiles.IndexOf(activeTile);

        activeTile.RemoveNumber();

        activeTile = tiles[index].SetNumber(index);

        Invoke(nameof(MoveNumber), index);
    }

    private void OnTapeClick()
    {
        Debug.Log("NAJAL");
    }
}
