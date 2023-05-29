using Architecture;

public class LifesRepository : Repository
{
    public float heartsScore { get; set; }
    public float maxHeartScore { get; private set; }
    public float startHeartScoreAmount { get; private set; }
    public float heartPointsForOnePoint { get; private set; }

    private LifesSettingsConfig _lifesSettingsConfig;

    public override void OnCreate()
    {
        heartsScore = 0f;
        _lifesSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<LifesSettingsConfig>();

        maxHeartScore = _lifesSettingsConfig.maxHeartScore;
        startHeartScoreAmount = _lifesSettingsConfig.startHeartScoreAmount;
        heartPointsForOnePoint = _lifesSettingsConfig.heartPointsForOnePoint;
    }

    public override void Initialize()
    {

    }

    public override void Save()
    {
        throw new System.NotImplementedException();
    }
}
