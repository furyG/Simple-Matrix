using System.Collections;
using UnityEngine;

public class NumberTypeHandler : MonoBehaviour, ITypeChangable<NumberType>
{
    public NumberType type => _type;
    private NumberType _type;

    private int _currentMaxNumberValue;
    private int _number;
    private NumberManager _manager;
    private NumberComplicationHandler _numberComplicationHandler;

    private void Awake()
    {
        _manager = GetComponent<NumberManager>();
        _numberComplicationHandler = _manager.numberComplicationHandler;
    }

    public NumberType SetType()
    {
        float chance = Random.Range(0f, 1f);
        _type = _numberComplicationHandler.GetNumberType(chance);

        ChooseNumber(_type);

        return _type;
    }
    private void ChooseNumber(NumberType type)
    {
        _currentMaxNumberValue = _numberComplicationHandler.GetNumbersMaxValue();
        switch (type)
        {
            case NumberType.simple:
                _number = Random.Range(1, _currentMaxNumberValue + 1);
                break;
            case NumberType.changing:
                _number = Random.Range(1, _currentMaxNumberValue + 1);
                StartCoroutine(InvokeNumberIncreaseInTime(1));
                break;
        }
        _manager.SetNumberValues(_number);
    }

    private IEnumerator InvokeNumberIncreaseInTime(int time)
    {
        yield return new WaitForSeconds(time);

        _number = Mathf.Clamp(++_number, 1, _currentMaxNumberValue);
        _manager.SetNumberValues(_number);

        StartCoroutine(InvokeNumberIncreaseInTime(1));
    }
}
