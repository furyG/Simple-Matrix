using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public class PointsRepository : Repository
    {
        public int points { get; set; }
        public int pointsForNextLevel { get; set; }
        public int pointsIncrement { get; private set; }

        public override void OnCreate()
        {
            points = 0;

            pointsForNextLevel = Balance.GetInstance().FirstRoundPointsCap;
            pointsIncrement = Balance.GetInstance().IncrementPointsCap;
        }

        public override void Initialize()
        {

        }

        public override void Save()
        {
            //save in playerprefs
        }
    }
}

