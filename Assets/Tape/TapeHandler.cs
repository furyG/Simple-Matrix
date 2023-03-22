using System;
using System.Collections.Generic;
using Tapes;
using Unity.VisualScripting;
using UnityEngine;

namespace Tapes
{
    public class TapeHandler : MonoBehaviour 
    {
        #region oldCode
        //public static TapeHandler instance;

        //public static List<Tape> tapes = new List<Tape>();
        //protected static List<float> preferredXPos = new List<float>();
        //protected static GameObject[,] numberGOs;

        //protected float buffer { get; private set; }

        //private void Awake()
        //{
        //    instance = this;
        //}

        //public void SetNumber(Number n)
        //{
        //    Transform number = n.transform;
        //    Tape t = n.pTape;
        //    n.boarded = true;

        //    buffer = number.localScale.x / 2 + 0.1f;

        //    float pos = number.localPosition.x;

        //    if (preferredXPos.Count == 0) AddColumn(t);
        //    else
        //    {
        //        float closest = preferredXPos[0];
        //        for (int i = 0; i < preferredXPos.Count; i++)
        //        {
        //            if (Mathf.Abs(preferredXPos[i] - pos) < Mathf.Abs(closest - pos))
        //            {
        //                closest = preferredXPos[i];
        //            }
        //        }
        //        if (Mathf.Abs(closest - pos) < buffer)
        //        {
        //            ChangeNumberOnPos(n.pTape, closest);

        //            Vector2 newNumPos = new(closest, 0);
        //            number.localPosition = newNumPos;
        //            number.SetSiblingIndex(preferredXPos.IndexOf(closest));
        //        }
        //        else
        //        {
        //            AddColumn(t);
        //        }
        //    }
        //    CheckBonus(t);
        //}
        //public void CheckBonus(Tape t)
        //{
        //    //float num = t.number.transform.localPosition.x;
        //    Bonus[] bs = t.transform.GetComponentsInChildren<Bonus>();
        //    if (bs.Length > 0)
        //    {
        //        for (int i = 0; i < bs.Length; i++)
        //        {
        //            float bPos = bs[i].transform.localPosition.x;
        //            if (Mathf.Abs(bPos - num) < buffer / 2)
        //            {
        //                bs[i].Interact();
        //                Destroy(bs[i].gameObject);
        //                break;
        //            }
        //        }
        //    }
        //}
        //private void AddColumn(Tape t)
        //{
        //    float x = t.number.transform.localPosition.x;
        //    preferredXPos.Add(x);
        //    preferredXPos.Sort();

        //    for (int i = 0; i < tapes.Count; i++)
        //    {
        //        if (tapes[i] == t) continue;

        //        Transform zero = new GameObject(0.ToString()).transform;
        //        zero.SetParent(tapes[i].transform);
        //        zero.localPosition = new(x, 0);
        //        zero.localScale = new(t.number.transform.localScale.x, 1);
        //        zero.SetSiblingIndex(preferredXPos.IndexOf(x));
        //        zero.AddComponent<SpriteRenderer>().sortingOrder = 1;
        //    }
        //    SortColumns();
        //}
        //protected void SortColumns()
        //{
        //    for (int i = 0; i < tapes.Count; i++)
        //    {
        //        for (int j = 0; j < preferredXPos.Count; j++)
        //        {
        //            Transform child = tapes[i].transform.GetChild(j);
        //            child.SetSiblingIndex(preferredXPos.IndexOf(child.transform.localPosition.x));
        //        }
        //    }
        //}
        //private void ChangeNumberOnPos(Tape t, float xPos)
        //{
        //    Destroy(t.transform.GetChild(preferredXPos.IndexOf(xPos)).gameObject);
        //}

        //public void HighlightNumbers(int x, int y)
        //{
        //    SpriteRenderer rend = numberGOs[x, y].GetComponent<SpriteRenderer>();
        //    Sprite s = rend.sprite;

        //    GameObject h = new("H");
        //    h.transform.SetParent(numberGOs[x, y].transform);
        //    h.transform.localScale *= 1.2f;
        //    h.transform.localPosition = Vector3.zero;
        //    SpriteRenderer hRend = h.AddComponent<SpriteRenderer>();
        //    hRend.sprite = s;

        //    Color randColor = new(UnityEngine.Random.Range(0f, 1f)
        //        , UnityEngine.Random.Range(0f, 1f)
        //        , UnityEngine.Random.Range(0f, 1f));

        //    hRend.color = randColor;
        //}
        #endregion oldCode
        private void Awake()
        {
            
        }

    }
}


