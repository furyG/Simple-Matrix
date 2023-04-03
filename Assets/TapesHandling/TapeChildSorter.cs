//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class TapeChildSorter
//{
//    //private Tape tape;

//    private Numberable lastNumber;
//    private List<Numberable> childrenNums;

//    //public TapeChildSorter(Tape tape)
//    //{
//    //    this.tape = tape;

//    //    childrenNums  = new List<Numberable>();
//    //}

//    public void SetNumber(Numberable lastNumber)
//    {
//        this.lastNumber = lastNumber;

//        if (CheckForClosest(lastNumber) == null)
//        {
//            childrenNums.Add(lastNumber);
//        }

//        SortChildren();
//    }
//    private Numberable CheckForClosest(Numberable lastNumber)
//    {
//        float buffer = lastNumber.transform.localScale.x / 2 + 0.1f;

//        Numberable closest = GetClosest(lastNumber);
//        if (closest != null)
//        {
//            float closestX = closest.transform.localPosition.x;

//            float delta = Mathf.Abs(closestX - lastNumber.transform.localPosition.x);
//            if (delta < buffer)
//            {
//                ChangeNumberOnPos(closest);
//            }
//            return closest;
//        }
//        return null;
//    }

//    private Numberable GetClosest(Numberable lastNumber)
//    {
//        if (childrenNums.Count < 1) return null;

//        float numberX = lastNumber.transform.localPosition.x; 

//        Numberable closest = childrenNums[0];

//        foreach (var number in childrenNums)
//        {
//            if (number.Equals(lastNumber)) continue;

//            float closestX = closest.transform.localPosition.x;
//            float onTapeNumberX = number.transform.localPosition.x;

//            if (Mathf.Abs(onTapeNumberX - numberX) < Mathf.Abs(closestX - numberX))
//            {
//                closest = number;
//            }
//        }
//        return closest;
//    }
//    private void ChangeNumberOnPos(Numberable closest)
//    {
//        Vector2 newNumPos = new(closest.transform.localPosition.x, 0);
//        closest.transform.localPosition = newNumPos;

//        int closestIndex = childrenNums.IndexOf(closest);
//        //tape.DestroyNumber(childrenNums[closestIndex]);
//        childrenNums.RemoveAt(closestIndex);

//        childrenNums.Insert(closestIndex, lastNumber);
//        childrenNums.RemoveAt(closestIndex);

//        //closest.Number = lastNumber.Number;
//        //tape.DestroyNumber(lastNumber);
//    }
//    private void SortChildren() => childrenNums = childrenNums.OrderBy(x => x.transform.localPosition.x).ToList();

//    //private Zero SpawnZero(float x)
//    //{
//    //    //Zero zero = TapeObjectsFactory.instance.Get<Zero>(//tape.transform);
//    //    zero.transform.localPosition = new(x, 0);

//    //    return zero;
//    //    //zero.transform.SetSiblingIndex(childrenNums.IndexOf(childrenNums.FirstOrDefault(index => index.transform.localPosition.x == x)));
//    //}

//    private bool CanAddZero(float x)
//    {
//        int index = childrenNums.IndexOf(childrenNums.FirstOrDefault(index => index.transform.localPosition.x == x));

//        if (childrenNums.Count <= index || index == -1) return true;

//        return !childrenNums[index];
//    }

//    public void TryAddZero(float x)
//    {
//        if (CanAddZero(x))
//        {
//            Zero zero = SpawnZero(x);

//            SetNumber(zero);
//            SortChildren();
//        }
//    }
//}
