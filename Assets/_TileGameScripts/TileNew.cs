using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileNew : MonoBehaviour
{
    [SerializeField] private Image _image;
    public bool active
    {
        get => _active;
    }
    public int value
    {
        get => _value;
        private set
        {
            _value = value;
            _image.sprite = numbersSprites[_value];
            if(value == 0)
            {
                _image.sprite = null;
            }
        }
    }

    private bool _active;
    [SerializeField] private int _value = 0;

    private Sprite[] numbersSprites;

    private void Awake()
    {
        numbersSprites = Resources.LoadAll<Sprite>("Sprites/numbers");
    }

    public TileNew SetNumber(int num)
    {
        _active = true;

        value = num;

        return this;
    }

    public void RemoveNumber()
    {
        _active = false;

        value = 0;
    }
}
