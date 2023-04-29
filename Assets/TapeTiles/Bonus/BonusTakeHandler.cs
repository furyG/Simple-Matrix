using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;

public class BonusTakeHandler
{
    private Dictionary<BonusType, IBonusReciever> bonusRecieversMap;

    private BonusManager _manager;
    private TimerInteractor timerInteractor;
    private LifesInteractor lifesInteractor;

    public BonusTakeHandler(BonusManager manager)
    {
        _manager = manager;
    }
    public void InitializeHandler()
    {
        timerInteractor = Game.GetInteractor<TimerInteractor>();
        lifesInteractor = Game.GetInteractor<LifesInteractor>();

        bonusRecieversMap = new Dictionary<BonusType, IBonusReciever>()
        {
            { BonusType.time, timerInteractor},
            { BonusType.life, lifesInteractor},
            { BonusType.slow, Balance.GetInstance()}
        };
    }
    public void TakeBonus(BonusType bonusType)
    {
        bonusRecieversMap[bonusType].TakeBonus();
    }
}
