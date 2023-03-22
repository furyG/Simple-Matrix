using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public class PointsRepository : Repository
    {
        public int points { get; set; }
        public int pointsForNextLevel { get; private set; }

        public int combo { get; set; }

        public override void OnCreate()
        {
        }

        public override void Initialize()
        {
            points = 0;
            combo = 1;
            pointsForNextLevel = Balance.instance.FirstRoundPointsCap;
        }

        public override void Save()
        {
            //save in playerprefs
        }
    }
}

