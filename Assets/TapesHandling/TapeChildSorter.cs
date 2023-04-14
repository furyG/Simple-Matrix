using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TapeChildSorter
{
    private TapeContentSpawner _contentSpawner;
    private TapeManager _manager;
    private List<NumberManager> _nums;

    public TapeChildSorter(TapeManager manager, TapeContentSpawner contentSpawner)
    {
        _contentSpawner = contentSpawner;
        _manager = manager;
        
        _nums = new List<NumberManager>();
    }
    private NumberManager GetClosestTo(float x)
    {
        NumberManager closest = _nums[0];

        foreach (var number in _nums)
        {
            float closestX = closest.transform.localPosition.x;
            float onTapeNumberX = number.transform.localPosition.x;

            if (Mathf.Abs(onTapeNumberX - x) < Mathf.Abs(closestX - x))
            {
                closest = number;
            }
        }
        return closest;
    }
    private void ReplaceNumber(NumberManager closest, NumberManager lastNumber)
    {
        closest.SetNumberValues(lastNumber.number);
        _manager.lastNumber = closest;

        UnityEngine.Object.Destroy(lastNumber.gameObject);
    }
    private List<NumberManager> AddNumberInList(NumberManager lastNumber)
    {
        lastNumber.SetNumberOnTape();
        _nums.Add(lastNumber);
        _nums = _nums.OrderBy(x => x.transform.localPosition.x).ToList();

        return _nums;
    }
    private NumberManager GetZeroFromSpawner(Vector2 spawnPos)
    {
        return _contentSpawner.SpawnNumber(NumberType.zero, spawnPos);
    }
    public List<NumberManager> GetSortedChilds(NumberManager lastNumber)
    {
        float numWidth = lastNumber.GetComponent<RectTransform>().rect.width;
        float buffer = numWidth / 2 + 60;

        if (_nums.Count >= 1)
        {
            NumberManager closest = GetClosestTo(lastNumber.transform.localPosition.x);
            float delta = Mathf.Abs(closest.transform.localPosition.x - lastNumber.transform.localPosition.x);
            if (delta < buffer)
            {
                ReplaceNumber(closest, lastNumber);
                return _nums;
            }
        }
        return AddNumberInList(lastNumber);
    }
    public List<NumberManager> CompareChildLists(List<NumberManager> targetList)
    {
        if (targetList.Count == _nums.Count) return _nums;

        for(int i = 0; i < targetList.Count; i++)
        {
            NumberManager num = _nums.FirstOrDefault(x => x.transform.localPosition.x == targetList[i].transform.localPosition.x);
            if (!num)
            {
                AddNumberInList(GetZeroFromSpawner(targetList[i].transform.localPosition));
            }
        }

        return _nums;
    }

}
