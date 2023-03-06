using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public class PointsRepository : Repository
    {
        public int points { get; set; }

        public override void Initialize()
        {
            
        }

        public override void Save()
        {
            //save in playerprefs
        }
    }
}

