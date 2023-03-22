using Architecture;

public class LevelRepository : Repository
{
    public int level { get; set; }
    public override void OnCreate()
    {
        level = 1;
    }
    public override void Initialize()
    {
        
    }

    public override void Save()
    {
        
    }
}
