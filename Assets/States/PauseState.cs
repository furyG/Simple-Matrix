using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IState
{
    private MainController main;
    private bool paused = true;

    public PauseState(MainController main)
    {
        this.main = main;
    }

    public void Enter()
    {
        paused = !paused;
        Time.timeScale = Convert.ToInt32(paused);
        if (!paused) Exit();
    }

    public void Exit()
    {
        paused = !paused;
        Time.timeScale = Convert.ToInt32(paused);
    }
}
