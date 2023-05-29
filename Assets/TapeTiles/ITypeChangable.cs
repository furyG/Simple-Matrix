public enum NumberType
{
    simple,
    changing
}

//not implemented
public enum BonusType
{
    time = 0,
    //random = 1,
    life = 2,
    slow = 3,
}

public interface ITypeChangable<T> where T : System.Enum
{
    T type { get; }
    T SetType();
}
