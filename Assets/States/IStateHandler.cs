using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateHandler<T> where T : class
{
    public StateChangeHandler _stateHandler { get; set; }
}
