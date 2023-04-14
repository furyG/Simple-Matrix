using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTypeHandler : MonoBehaviour, ITypeChangable<NumberType>
{
    private int _number;
    private NumberManager _manager;

    private void Awake()
    {
        _manager= GetComponent<NumberManager>();
    }

    public void SetType(NumberType type = NumberType.simple)
    {
        if(type != NumberType.zero)
        {
            float chance = Random.Range(0f, 1f);
            if (chance > 0.8f)
            {
                type = NumberType.changing;
            }
        }

        ChooseNumber(type);
    }
    private void ChooseNumber(NumberType type)
    {
        switch (type)
        {
            case NumberType.zero:
                _number = 0;
                break;
            case NumberType.simple:
                _number = Random.Range(1, 7);
                break;
            case NumberType.changing:
                _number = 1;
                StartCoroutine(Utils.InvokeRoutine(IncreaseNum, 1));
                break;
        }
        _manager.SetNumberValues(_number);
    }
    private void IncreaseNum()
    {
        if (_manager.boarded) return;

        _number = _manager.SetNumberValues(++_number);
        StartCoroutine(Utils.InvokeRoutine(IncreaseNum, 1));
    }
}
