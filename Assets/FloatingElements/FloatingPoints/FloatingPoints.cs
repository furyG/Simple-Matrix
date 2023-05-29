using UnityEngine.UI;

//not implemented
public class FloatingPoints : FloatingElement
{
    public int points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            scoreString = _points.ToString("N0");

            vizualizer.text = scoreString;
        }
    }

    private Text vizualizer;
    private string scoreString;

    protected int _points = 0;


    private void Awake()
    {
        vizualizer = GetComponent<Text>();
    }
    public override void FPCallback(FloatingPoints fp)
    {
        points += fp.points;
    }
    protected override void ToggleVizualizer(bool state)
    {
        vizualizer.enabled = state;
    }
    protected override void ChangeFont(int size)
    {
        vizualizer.fontSize = size;
    }
}
