using UnityEngine;
using UnityEngine.UI;

//not implemented
public class BonusRenderer : RendererBase
{
    public Image visualizer => GetComponent<Image>();

    public BaseColors colors { get; set; }

    private Sprite[] _bonusSprites;

    private void Awake()
    {
        InitializeSprites();
    }

    public override void ChangeColor(Color color)
    {
        visualizer.color = color;
    }

    public override void ChangeSprite(Sprite sprite)
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
