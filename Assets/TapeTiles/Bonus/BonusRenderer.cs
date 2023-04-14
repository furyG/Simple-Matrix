using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusRenderer : MonoBehaviour, IRenderer
{
    public Image image => GetComponent<Image>();

    private Sprite[] _bonusSprites;

    private void Awake()
    {
        InitializeSprites();
    }

    public void ChangeColor(Color color)
    {
        image.color = color;
    }

    public void ChangeSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void ChangeSprite(int numberInArray)
    {
        image.sprite = _bonusSprites[numberInArray];
    }

    public void InitializeSprites()
    {
        _bonusSprites = Resources.LoadAll<Sprite>("Sprites/bonuses");
    }
}
