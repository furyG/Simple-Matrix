using System.Collections;
using System.Collections.Generic;
using Tapes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayState : IState
{
    private MainController main;
    private Transform tapeAnchor;
    private TapeSpawner tSpawner;

    public PlayState(MainController main)
    {
        this.main = main;

        CreateTapeAnchor();
    }
    private void CreateTapeAnchor()
    {
        tapeAnchor = new GameObject("TapeAnchor").transform;
        tSpawner = tapeAnchor.AddComponent<TapeSpawner>();
    }
    public void Enter()
    {
        tSpawner.SpawnTapes(Balance.instance.StartTapesAmount);
    }

    public void Update()
    {
        if(main.Timer.LeftTime < 0)
        {
            main.MainStateMachine.TransitionTo(main.MainStateMachine.countState);
        }
    }

    public void Exit()
    {
        foreach(Tape t in TapeHandler.tapes)
        {
            t.StopTape();
        }
    }
}
