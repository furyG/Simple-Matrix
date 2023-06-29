public class TapeManager : Observer
{
    public NumberManager lastNumber
    {
        get => _lastNumber;
        set => _lastNumber = value;
    }
    private NumberManager _lastNumber;

    private TapeContentSpawner _spawner;

    private TapeNumberSetter _tapeNumberSetter;

    private void Awake()
    {
        _spawner = GetComponent<TapeContentSpawner>();
    }
    private void Start()
    {
        _tapeNumberSetter = new TapeNumberSetter(_spawner.tilesNeighbours);
    }

    public override void Notify(Subject subject)
    {
        ClickCheck();
    }
    public void StartContentSpawning()
    {
        _spawner.InvokeSpawnContent();
    }
    public void StopContentSpawning()
    {
        _spawner.CancelContentSpawning();
    }
    public void ClearTapeContent()
    {
        _tapeNumberSetter.ClearAllNeighbours();
        lastNumber?.gameObject.SetActive(false);
    }
    private void ClickCheck()
    {
        if (!_spawner.lastSpawnedNumber) return;
        if (!_spawner.lastSpawnedNumber.isActiveAndEnabled) return;

        lastNumber = _spawner.lastSpawnedNumber;
        _tapeNumberSetter.SetNumber(lastNumber);

        _spawner.InvokeSpawnContent();
    }
}
