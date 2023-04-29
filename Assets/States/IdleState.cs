using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private GameModeManager main;
    public IdleState(GameModeManager main)
    {
        this.main = main;
    }
    public void Enter()
    {
        //приветствие?
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        //переход?
    }
}
