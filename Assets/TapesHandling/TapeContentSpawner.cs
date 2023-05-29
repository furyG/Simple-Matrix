using Architecture;
using System.Collections.Generic;
using UnityEngine;

public class TapeContentSpawner : MonoBehaviour
{
    public NumberManager lastSpawnedNumber { get; set; }
    public List<TileNeighbour> tilesNeighbours { get; private set; }

    [SerializeField] private int _poolCount = 1;
    [SerializeField] private bool _autoExpand = false;

    private float _spawnNumberTime;
    private int _tilesOnTape;

    private PoolMono<NumberManager> _pool;
    private TapeSettingsConfig _tapeSettingsConfig;

    private const int tileThreshold = 119;

    private void Awake()
    {
        _tapeSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<TapeSettingsConfig>();

        _spawnNumberTime = _tapeSettingsConfig.spawnNumberInTime;
        _tilesOnTape = _tapeSettingsConfig.tilesOnTape;
    }

    private void Start()
    {
        this._pool = new PoolMono<NumberManager>(_poolCount, transform);
        this._pool.autoExpand = _autoExpand;

        InvokeSpawnContent();
    }
    private void SpawnContent()
    {
        CreateNumber();
    }
    private void CreateNumber()
    {
        if (lastSpawnedNumber)
        {
            lastSpawnedNumber.GetComponent<IMoveable>().OnMovingEnd -= InvokeSpawnContent;
        }
        lastSpawnedNumber = _pool.GetFreeElement();
        lastSpawnedNumber.InitializeNumber();

        lastSpawnedNumber.GetComponent<IMoveable>().OnMovingEnd += InvokeSpawnContent;
    }

    private Tile SpawnTile(int xPosition)
    {
        Tile t = TapeObjectsFactory.GetInstance().Get<Tile>(transform);
        t.transform.localPosition = new(xPosition, 0);

        return t;
    }
    public void InvokeSpawnContent()
    {
        if (!isActiveAndEnabled) return;

        StartCoroutine(Utils.InvokeRoutine(SpawnContent, _spawnNumberTime));
    }
    public List<TileNeighbour> SpawnTiles()
    {
        tilesNeighbours = new List<TileNeighbour>();
        for(int i = -_tilesOnTape/2; i < _tilesOnTape/2; i++)
        {
            Tile t = SpawnTile(tileThreshold * i + (tileThreshold / 2));
            t.name = (i + _tilesOnTape / 2).ToString();

            tilesNeighbours.Add(t.tileNeigbour);
        }
        return tilesNeighbours;
    }
    public void CancelContentSpawning()
    {
        StopAllCoroutines();
        if (lastSpawnedNumber) Destroy(lastSpawnedNumber.gameObject);
    }
}
