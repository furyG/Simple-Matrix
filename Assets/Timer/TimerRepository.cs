using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRepository : Repository
{
    public float leftTime { get; set; }
    public float originRoundTime { get; private set; }
    public float startGameTime { get; set; }

    public override void Initialize()
    {
        originRoundTime = Balance.instance.FirstRoundTime;
    }

    public override void Save()
    {
        //save in playerprefs
    }
}
