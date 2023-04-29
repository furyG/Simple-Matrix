using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberRenderer : MonoBehaviour, IRenderer<TextMeshProUGUI>
{
    [SerializeField] private BaseColors colors;
    public TextMeshProUGUI visualizer => GetComponent<TextMeshProUGUI>();

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
        
    }
    public void ChangeSprite(int number)
    {
        visualizer.text = number.ToString();
    }

    public void InitializeSprites()
    {
        
    }
    public void ChangeColorByType(NumberType type)
    {
        switch(type)
        {
            case NumberType.simple:
                ChangeColor(colors.orange);
                break;
            case NumberType.changing:
                ChangeColor(colors.black);
                break;
            default: 
                ChangeColor(colors.black);
                break;
        }
    }
}
