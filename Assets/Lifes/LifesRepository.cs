using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesRepository : Repository
{
    public int lifes { get; set; }
    public int maximumLifes { get; private set; }
    public override void Initialize()
    {
        lifes = 0;
        maximumLifes = Balance.instance.MaximumLifes;
    }

    public override void Save()
    {
        throw new System.NotImplementedException();
    }
}
