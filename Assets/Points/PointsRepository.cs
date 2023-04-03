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

        public int combo { get; set; }

        public override void OnCreate()
        {
            points = 0;
            combo = 1;

            pointsForNextLevel = Balance.instance.FirstRoundPointsCap;
            pointsIncrement = Balance.instance.IncrementPointsCap;
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

