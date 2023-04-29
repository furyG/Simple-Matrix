using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateChangeReciever
{
    public StateChangeHandler stateChangeHandler { get; set; }
    public void OnStateChanged(IState state);
}
