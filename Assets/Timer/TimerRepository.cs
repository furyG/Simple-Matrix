using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRepository : Repository
{
    public float leftTime { get; set; }
    public float originLevelTime { get; private set; }
    public float levelUpIncrementTime { get; private set; }
    public float timerBonusIncrementAmount { get; private set; }
    public float startGameTime { get; set; }

    public override void Initialize()
    {
        originLevelTime = Balance.instance.FirstLevelTime;
        levelUpIncrementTime = Balance.instance.LevelUpTimerIncrement;
        timerBonusIncrementAmount = Balance.instance.TimerBonusIncrementAmount;
    }

    public override void Save()
    {
        //save in playerprefs
    }
}
