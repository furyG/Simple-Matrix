using System;
using System.Collections.Generic;
using UnityEngine;

public class TapeManager : MonoBehaviour
{
    public event Action<TapeManager, List<NumberManager>> OnChildSort;
    public event Action<TapeManager> OnManagerDestroy;
    public NumberManager lastNumber
    {
        get => _lastNumber;
        set => _lastNumber = value;
    }
    private NumberManager _lastNumber;

    private TapeChildSorter _sorter;
    private TapeClickHandler _clickHandler;
    private TapeContentSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<TapeContentSpawner>();
        _clickHandler = GetComponent<TapeClickHandler>();

        _sorter = new TapeChildSorter(this, _spawner);
    }
    private void Start()
    {
        _clickHandler.OnTapeClicked += ClickCheck;
    }
    private void OnDestroy()
    {
        OnManagerDestroy?.Invoke(this);
    }
    private void ClickCheck()
    {
        if (_lastNumber == null) return;

        List<NumberManager> nums = _sorter.GetSortedChilds(lastNumber);
        OnChildSort?.Invoke(this, nums);

        _spawner.InvokeSpawnContent();

        _lastNumber = null;
    }
    public List<NumberManager> CompareChildLists(List<NumberManager> targetList)
    {
        return _sorter.CompareChildLists(targetList);
    }
    public void StopTape()
    {
        _spawner.CancelContentSpawning();
    }
}
