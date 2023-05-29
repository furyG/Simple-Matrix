using Architecture;
using TMPro;
using UnityEngine;

public class NumberRenderer : RendererBase
{
    public TextMeshProUGUI visualizer => GetComponent<TextMeshProUGUI>();

    private NumberSettingsConfig _numberSettingsConfig;

    private void Awake()
    {
        _numberSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<NumberSettingsConfig>();
    }
    public override void ChangeColor(Color color)
    {
        visualizer.color = color;
    }
    public void ChangeSprite(int number)
    {
        visualizer.text = number.ToString();
    }
    public void ChangeColorByType(NumberType type)
    {
        ChangeColor(_numberSettingsConfig.GetNumberColorByType(type));
    }
}
