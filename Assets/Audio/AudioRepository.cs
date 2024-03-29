using Architecture;
using UnityEngine;

public class AudioRepository : Repository
{
    private SoundsContainer _soundsContainer;
    public SoundsContainer soundsContainer => _soundsContainer;

    public override void Initialize()
    {
        _soundsContainer = Resources.Load<SoundsContainer>("Sounds/SoundsContainer");
    }

    public override void Save()
    {
        
    }
}
