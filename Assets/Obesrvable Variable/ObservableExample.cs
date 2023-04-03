using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableExample : MonoBehaviour
{
    private ObservableVariable<int> _obesrvableVariable;
    private ObservableLogger _logger;

    private void Start()
    {
        _obesrvableVariable= new ObservableVariable<int>(10);
        
        _logger= new ObservableLogger(_obesrvableVariable);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeInt();
        }
    }
    private void RandomizeInt()
    {
        var rInt = Random.Range(0, 100);
        _obesrvableVariable.Value = rInt;
    }
}
