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
            ChangeScale();
        }
    }
    protected int _heartPoints = 0;

    private void Awake()
    {
        vizualizer = GetComponent<Image>();
    }

    public override void FPCallback(FloatingPoints fp)
    {
        
    }
    protected override void ToggleVizualizer(bool state)
    {
        vizualizer.enabled = state;
    }
    public override void ChangeScale()
    {
        
    }
}
