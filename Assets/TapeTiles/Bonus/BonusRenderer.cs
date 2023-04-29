using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusRenderer : MonoBehaviour, IRenderer<Image>
{
    public Image visualizer => GetComponent<Image>();

    public Color32 GAME_BLACK => throw new System.NotImplementedException();

    public Color32 GAME_RED => throw new System.NotImplementedException();

    public BaseColors colors { get; set; }

    private Sprite[] _bonusSprites;

    private void Awake()
    {
        InitializeSprites();
    }

    public void ChangeColor(Color color)
    {
        visualizer.color = color;
    }

    public void ChangeSprite(Sprite sprite)
    {
        visualizer.sprite = sprite;
    }

    public void ChangeSprite(int numberInArray)
    {
        visualizer.sprite = _bonusSprites[numberInArray];
    }

    public void InitializeSprites()
    {
        _bonusSprites = Resources.LoadAll<Sprite>("Sprites/bonuses");
    }
}
