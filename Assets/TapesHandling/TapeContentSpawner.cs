using Architecture;
using System.Collections;
using UnityEngine;

public class TapeContentSpawner : MonoBehaviour
{
    private float _bonusSpawnChance;
    private float _spawnNumberTime;
    private float _numberRunDuration;

    private Balance _balance;
    private TapeManager _manager;
    private NumberManager _lastSpawnedNumber;
    private TimerInteractor _timerInteractor;

    private void Awake()
    {
        _balance = Balance.instance;
        _manager = GetComponent<TapeManager>();
        _timerInteractor = Game.GetInteractor<TimerInteractor>();

        _bonusSpawnChance = _balance.BonusSpawnChance;
        _spawnNumberTime = _balance.SpawnNumbersTime;
        _numberRunDuration = _balance.NumberRunDuration;
    }
    private void Start() => InvokeSpawnContent();
    private void SpawnContent()
    {
        _timerInteractor.StartRoundTimer();

        SpawnNumber();

        float chance = Random.Range(0f, 1f);
        if (chance < _bonusSpawnChance)
        {
            TapeObjectsFactory.instance.Get<BonusManager>(transform);
        }
    }
    public void InvokeSpawnContent()
    {
        if (!this.isActiveAndEnabled) return;

        StartCoroutine(Utils.InvokeRoutine(SpawnContent, _spawnNumberTime));
    }
    public NumberManager SpawnNumber(NumberType type = NumberType.simple, Vector2 spawnPos = default)
    {
        _lastSpawnedNumber = TapeObjectsFactory.instance.Get<NumberManager>(transform);
        _lastSpawnedNumber.InvokeNumber(spawnPos, type);

        if(type != NumberType.zero)
        {
            _manager.lastNumber = _lastSpawnedNumber;
            _lastSpawnedNumber.GetComponent<IMoveable<NumberType>>().OnMovingEnd += InvokeSpawnContent;
        }

        return _lastSpawnedNumber;
    }
    public void CancelContentSpawning()
    {
        StopAllCoroutines();
        if (_lastSpawnedNumber) Destroy(_lastSpawnedNumber.gameObject);
    }
    
}
