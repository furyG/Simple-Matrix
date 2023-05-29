using Architecture;
using UnityEngine;

public class NumberComplicationHandler
{
    public int currentPointsAmount { get => _currentPointsAmount; }
    private int _currentPointsAmount;

    private float _defaultRunDuration;
    private float _runDurationDecrement;
    private float _minRunDuration;

    private NumberManager _numberManager;
    private PointsInteractor _pointsInteractor;
    private NumberSettingsConfig _numberRunSettingsConfig;

    public NumberComplicationHandler(NumberManager numberManager)
    {
        _numberManager = numberManager;
        _pointsInteractor = Game.GetInteractor<PointsInteractor>();
        _numberRunSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<NumberSettingsConfig>();

        _defaultRunDuration = _numberRunSettingsConfig.numberRunDuration;
        _runDurationDecrement = _numberRunSettingsConfig.runDurationDecrement;
        _minRunDuration = _numberRunSettingsConfig.minNumberRunDuration;

        if(_pointsInteractor != null)
        {
            _pointsInteractor.pointsChanged += OnPointsChangedEvent;
        }
    }

    private void OnPointsChangedEvent(int points)
    {
        _currentPointsAmount = points;
    }

    public float GetRunDuration()
    {
        float runDuration = _defaultRunDuration - currentPointsAmount * _runDurationDecrement;
        runDuration = Mathf.Clamp(runDuration, _minRunDuration, _defaultRunDuration);
        return runDuration;
    }
    public NumberType GetNumberType(float chance)
    {
        foreach(NumberType type in (NumberType[]) System.Enum.GetValues(typeof(NumberType)))
        {
            if (type == NumberType.simple) continue;

            float typeChance = GetTypeSpawnChance(type);
            if(typeChance > chance)
            {
                return type;
            }
        }
        return NumberType.simple;
    }
    public int GetNumbersMaxValue()
    {
        int maxValue = _numberRunSettingsConfig.startNumbersMaxValue;
        maxValue += Mathf.FloorToInt(currentPointsAmount / _numberRunSettingsConfig.pointsForNumbersMaxValueIncrement);

        maxValue = Mathf.Clamp(maxValue, _numberRunSettingsConfig.startNumbersMaxValue, _numberRunSettingsConfig.maxNumberValue);

        return maxValue;
    }
    private float GetTypeSpawnChance(NumberType type)
    {
        NumberTypeConfig config = _numberRunSettingsConfig.GetNumberConfigByType(type);
        float spawnChance = config.defaultSpawnChance;

        spawnChance += _numberRunSettingsConfig.chanceSetIncrement * currentPointsAmount;

        spawnChance = Mathf.Clamp(spawnChance, config.defaultSpawnChance, config.maxSpawnChance);

        return spawnChance;
    }
}
