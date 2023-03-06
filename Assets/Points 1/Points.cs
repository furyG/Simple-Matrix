using System;

namespace Architecture
{
    public static class Points
    {
        public static event Action OnPointsInitializedEvent;

        public static int points
        {
            get
            {
                CheckClass();
                return pointsInteractor.points;
            }
        }
        public static bool isInitialized { get; private set; }

        private static PointsInteractor pointsInteractor;
        public static void Initialize(PointsInteractor interactor)
        {
            pointsInteractor = interactor;
            isInitialized= true;

            OnPointsInitializedEvent?.Invoke();
        }
        public static bool IsEnoughPoints(int value)
        {
            CheckClass();
            return pointsInteractor.IsEnoughPoints(value);
        }

        public static void AddPoints(object sender, int value)
        {
            CheckClass();
            pointsInteractor.AddPoints(sender, value);
        }
        public static void LosePoints(object sender, int value)
        {
            CheckClass();
            pointsInteractor.LosePoints(sender, value);
        }
        public static void CheckClass()
        {
            if (!isInitialized)
            {
                throw new Exception("Bank is not initialized yet!");
            }
        }
    }

}

