using UnityEngine;

public class NumberManager : MonoBehaviour
{
    public NumberComplicationHandler numberComplicationHandler { get; private set; }

    public int number => _number;
    private int _number;

    private NumberRenderer _renderer;
    private ITypeChangable<NumberType> _typeHandler;

    private void Awake()
    {
        numberComplicationHandler = new NumberComplicationHandler(this);

        _renderer = GetComponent<NumberRenderer>();
        _typeHandler = GetComponent<ITypeChangable<NumberType>>();
    }

    public void InitializeNumber()
    {
        _renderer.ChangeColorByType(_typeHandler.SetType());
    }
    public int SetNumberValues(int number)
    {
        _number = number;
        _renderer.ChangeSprite(number);

        return number;
    }
}
