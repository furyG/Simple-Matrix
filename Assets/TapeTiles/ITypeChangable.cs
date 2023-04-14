using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NumberType
{
    simple,
    allNums,
    changing,
    zero
}

public enum BonusType
{
    time = 0,
    //random = 1,
    life = 2,
    slow = 3,
}

public interface ITypeChangable<T> where T : System.Enum
{
    void SetType(T t);
}
