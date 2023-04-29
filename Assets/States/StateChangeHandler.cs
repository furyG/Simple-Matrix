using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeHandler
{
    private readonly IStateChangeReciever _manager;
    private readonly StateMachine _stateMachine;

    public StateChangeHandler(IStateChangeReciever manager, StateMachine mainStateMachine)
    {
        _manager = manager;
        _stateMachine = mainStateMachine;

        if (_stateMachine != null)
        {
            _stateMachine.onStateChanged += StateChangedEvent;
        }
    }

    private void StateChangedEvent(IState state) => _manager.OnStateChanged(state);
    
}
