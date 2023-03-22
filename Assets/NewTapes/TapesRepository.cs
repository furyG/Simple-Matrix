using Architecture;
using System.Collections.Generic;
using Tapes;
using UnityEngine;

public class TapesRepository : Repository
{
    public List<Tape> tapes { get; set; }
    public int tapesAmount { get; private set; }
    public List<List<Numberable>> numberables { get; set; }

    public List<float> preferredXPos { get; set; }

    public override void Initialize()
    {
        tapes = new List<Tape>();
        preferredXPos = new List<float>();
        tapesAmount = Balance.instance.StartTapesAmount;
    }

    public override void Save()
    {
        
    }
}
