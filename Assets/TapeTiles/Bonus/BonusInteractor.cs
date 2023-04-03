using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;
using System;

public enum bonusType
{
    time = 0,
    //random = 1,
    life = 1,
    slow = 2,
}

public class BonusInteractor : Interactor
{
    private Dictionary<bonusType, IBonusReciever> bonusRecieversMap;

    private TimerInteractor timerInteractor;
    private LifesInteractor lifesInteractor;

    public override void Initialize()
    {
        this.timerInteractor = Game.GetInteractor<TimerInteractor>();
        this.lifesInteractor = Game.GetInteractor<LifesInteractor>();

        bonusRecieversMap = new Dictionary<bonusType, IBonusReciever>()
        {
            { bonusType.time, timerInteractor},
            { bonusType.life, lifesInteractor},
            { bonusType.slow, Balance.instance}
        };
    }

    public void TakeBonus(bonusType bonusType)
    {
        bonusRecieversMap[bonusType].TakeBonus();
    }
}
