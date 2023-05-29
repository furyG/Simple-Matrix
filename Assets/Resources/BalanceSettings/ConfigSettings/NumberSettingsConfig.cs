using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Number Settings Config", menuName = "Gameplay/number config")]
public class NumberSettingsConfig : ScriptableObject
{
    [SerializeField] private float _numberRunDuration;
    [SerializeField] private float _slowNumberRunDuration;
    [SerializeField] private float _runDurationDecrement;
    [SerializeField] private float _minNumberRunDuration;
    [SerializeField] private float _chanceSetIncrement;
    [SerializeField] private int _startNumbersMaxValue;
    [SerializeField] private int _maxNumberValue;
    [SerializeField] private int _pointsForNumbersMaxValueIncrement;

    public float numberRunDuration => _numberRunDuration;
    public float slowNumberRunDuration => _slowNumberRunDuration;
    public float runDurationDecrement => _runDurationDecrement;
    public float minNumberRunDuration => _minNumberRunDuration;
    public float chanceSetIncrement => _chanceSetIncrement;
    public int startNumbersMaxValue => _startNumbersMaxValue;
    public int maxNumberValue => _maxNumberValue;
    public int pointsForNumbersMaxValueIncrement => _pointsForNumbersMaxValueIncrement;


    [SerializeField] private List<NumberTypeConfig> numberTypeConfigsList;

    private Dictionary<NumberType, NumberTypeConfig> numberTypesMap;

    private void OnEnable()
    {
        numberTypesMap = new Dictionary<NumberType, NumberTypeConfig>();
        foreach (var config in numberTypeConfigsList)
        {
            numberTypesMap.Add(config.type, config);
        }
    }
    public NumberTypeConfig GetNumberConfigByType(NumberType type)
    {
        if (numberTypesMap[type] != null)
        {
            return numberTypesMap[type];
        }
        else
        {
            Debug.Log("There is no config of this type");
            return null;
        }
    }
    public Color GetNumberColorByType(NumberType type)
    {
        NumberTypeConfig config = GetNumberConfigByType(type);
        return config.typeColor;
    }
}
