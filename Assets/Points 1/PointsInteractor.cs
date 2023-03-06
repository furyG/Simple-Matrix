namespace Architecture
{
    public class PointsInteractor : Interactor
    {
        private PointsRepository repository;

        public int points => this.repository.points;

        public override void OnCreate()
        {
            this.repository = Game.GetRepository<PointsRepository>();
        }
        public override void Initialize()
        {
            Points.Initialize(this);
        }

        public bool IsEnoughPoints(int value) => points >= value;

        public void AddPoints(object sender, int value)
        {
            this.repository.points += value;
        }
        public void LosePoints(object sender, int value)
        {
            this.repository.points -= value;
        }
    }

}

