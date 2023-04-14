using UnityEngine;

public class NumberManager : MonoBehaviour
{
    public Neighbour neighbour => _neighbour;
    private Neighbour _neighbour;

    public int number => _number;
    private int _number;

    public bool boarded = false;

    private IMoveable<NumberType> _mover;
    private IRenderer _renderer;
    private ITypeChangable<NumberType> _typeHandler;

    private void Awake()
    {
        _renderer = GetComponent<IRenderer>();
        _mover = GetComponent<IMoveable<NumberType>>();
        _typeHandler = GetComponent<ITypeChangable<NumberType>>();
        _neighbour = new Neighbour(this);
    }
    public void InvokeNumber(Vector2 pos = default, NumberType type = NumberType.simple)
    {
        _typeHandler.SetType(type);

        _mover.InvokeMoving(pos);
        _renderer.ChangeColor(Color.red);
    }
    public void SetNumberOnTape()
    {
        boarded = true;
        _mover.EndMove();

        _renderer.ChangeColor(Color.gray);
    }
    public int SetNumberValues(int number)
    {
        _number = number;
        name = number.ToString();
        _renderer.ChangeSprite(number);

        return number;
    }
}
