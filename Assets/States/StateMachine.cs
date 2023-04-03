using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

[Serializable]
public class StateMachine 
{
    public IState CurrentState { get; private set; }

    public IdleState idleState;
    public PlayState playState;
    public CountState countState;

    public event Action <IState> onStateChanged;

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

        onStateChanged?.Invoke(CurrentState);
        Debug.Log("Transition to: "+nextState.ToString());
    }
    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
    public StateMachine(MainController main)
    {
        this.idleState = new IdleState(main);
        this.playState = new PlayState(main);
        this.countState = new CountState(main);
    }

}
