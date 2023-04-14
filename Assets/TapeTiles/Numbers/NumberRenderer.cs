using UnityEngine;
using UnityEngine.UI;

public class NumberRenderer : MonoBehaviour, IRenderer
{
    public Image image => GetComponent<Image>();

    private Sprite[] numsSprites;

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
    public void ChangeSprite(int number)
    {
        ChangeSprite(numsSprites[number]);
    }

    public void InitializeSprites()
    {
        numsSprites = Resources.LoadAll<Sprite>("Sprites/numbers");
    }
}
