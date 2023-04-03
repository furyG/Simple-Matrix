using System;
using Tapes;
using UnityEngine;

//public class TapeOld : MonoBehaviour
//{
//    public event Action<Tape> tapeClicked;
//    public Numberable Number => _currentNumber;

//    private float bonusSpawnChance;
//    private float spawnNumbersTime;

//    private Numberable _currentNumber;
//    private Balance balance = Balance.instance;
//    private TapeChildSorter childSorter;

//    private void Start()
//    {
//        this.bonusSpawnChance = balance.BonusSpawnChance;
//        this.spawnNumbersTime = balance.SpawnNumbersTime;
//        //this.childSorter = new TapeChildSorter(this);

//        Invoke(nameof(SpawnNumber), spawnNumbersTime);
//    }

//    private void SpawnNumber()
//    {
//        _currentNumber = TapeObjectsFactory.instance.Get<Number>(transform);

//        float chance = UnityEngine.Random.Range(0f, 1f);
//        if (chance < bonusSpawnChance)
//        {
//            TapeObjectsFactory.instance.Get<Bonus>(transform);
//        }
//    }

//    protected void OnMouseDown()
//    {
//        if (_currentNumber == null) return;

//        _currentNumber.SetNumberable();

//        childSorter.SetNumber(_currentNumber);
//        tapeClicked?.Invoke(this);

//        Invoke(nameof(SpawnNumber), spawnNumbersTime);
//    }

//    public void TryAddZero(float x)
//    {
//        childSorter.TryAddZero(x);
//    }
//    public void DestroyNumber(Numberable num)
//    {
//        Destroy(num.gameObject);
//    }
//}
