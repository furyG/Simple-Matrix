using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;

public class GameOverState : IState
{
    private GameModeManager main;
    public GameOverState(GameModeManager main)
    {
        this.main = main;
    }
    public void Enter()
    {
        Timer.Stop();
    }

    public void Update()
    {
        
    }

    public void Exit()
    {

    }
}
