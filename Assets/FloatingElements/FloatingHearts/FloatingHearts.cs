using UnityEngine.UI;

public class FloatingHearts : FloatingElement
{
    private Image vizualizer;

    public int heartPoints
    {
        get
        {
            return _heartPoints;
        }
        set
        {
            _heartPoints = value;
        }
    }
    protected int _heartPoints = 0;

    private void Awake()
    {
        vizualizer = GetComponent<Image>();
    }

    public override void FPCallback(FloatingPoints fp)
    {
        base.FPCallback(fp);
    }
    protected override void ToggleVizualizer(bool state)
    {
        vizualizer.enabled = state;
    }
}
