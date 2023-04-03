using Architecture;
using System.Collections.Generic;
using Tapes;

public class TapesRepository : Repository
{
    public List<Tape> tapes { get; set; }
    public List<List<Numberable>> numberables { get; set; }

    public override void Initialize()
    {
        tapes = new List<Tape>();
    }

    public override void Save()
    {
        
    }
}
