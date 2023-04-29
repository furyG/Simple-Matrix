using UnityEngine;

public class NumberManager : MonoBehaviour
{
    public int number => _number;
    private int _number;

    public bool boarded = false;

    private NumberRenderer _renderer;
    private ITypeChangable<NumberType> _typeHandler;

    private void Awake()
    {
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
