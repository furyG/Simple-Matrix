using Architecture;

public class TimerRepository : Repository
{
    public float leftTime { get; set; }
    public float originLevelTime { get; private set; }
    public float timerIncrementForPoint { get; private set; }
    public float timerBonusIncrementAmount { get; private set; }
    public float startGameTime { get; set; }

    private RoundSettingsConfig _roundSettingsConfig;
    private BonusSettingsConfig _bonusSettingsConfig;

    public override void Initialize()
    {
        _roundSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<RoundSettingsConfig>();
        _bonusSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<BonusSettingsConfig>();

        originLevelTime = _roundSettingsConfig.firstRoundTime;
        timerIncrementForPoint = _roundSettingsConfig.timerIncrementForPoint;
        timerBonusIncrementAmount = _bonusSettingsConfig.timeBonusAdditionalTime;
    }

    public override void Save()
    {
        //save in playerprefs
    }
}
