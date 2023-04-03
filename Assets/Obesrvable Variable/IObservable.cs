using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    event Action<object> OnChanged;
}
