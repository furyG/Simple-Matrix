namespace Architecture
{
    public class PointsRepository : Repository
    {
        public int points { get; set; }
        public int pointsForNumber { get; private set; }

        private PointsSettingsConfig _pointsSettingsConfig;

        public override void Initialize()
        {
            points = 0;

            _pointsSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<PointsSettingsConfig>();

            pointsForNumber = _pointsSettingsConfig.pointsForNumber;
        }

        public override void Save()
        {
            //save in playerprefs
        }
    }
}

