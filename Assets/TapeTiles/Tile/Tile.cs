using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Tile : MonoBehaviour
{
    public TileNeighbour tileNeigbour { get; private set; }

    [SerializeField] private BaseColors baseColors;

    private TextMeshProUGUI _vizualizer;

    private void Awake()
    {
        _vizualizer = GetComponent<TextMeshProUGUI>();
        _vizualizer.color = baseColors.black;

        tileNeigbour = new TileNeighbour(this);     
    }
    public void SetNumber(int number)
    {
        _vizualizer.text = number.ToString();
    }
}
