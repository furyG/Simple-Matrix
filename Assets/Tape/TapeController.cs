using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapeController : MonoBehaviour
{
    public List<Tape> tapes = new List<Tape>();
    public List<float> preferredXPos = new List<float>();

    private GameObject[,] numberGOs;
    private float buffer;

    public void SpawnTapes(Transform tapeAnchor, GameObject tapePrefab, int tapeAmount)
    {
        float height = Camera.main.orthographicSize;

        Vector3 tapeSize = tapePrefab.transform.localScale;

        for (int i = tapeAmount - 1; i > -1; i--)
        {
            GameObject tapeGO = Instantiate(tapePrefab, tapeAnchor);
            tapeGO.transform.localScale = new Vector3(tapeSize.x
            , tapeSize.y - 0.1f * tapeAmount, tapeSize.y);
            Vector2 tapePos = new(0, (height / tapeAmount + 1) * i);
            tapeGO.transform.position = tapePos;

            float tTime = UnityEngine.Random.Range(1, 4);
            tapeGO.GetComponent<Tape>().InvokeTape(tTime);

            tapes.Add(tapeGO.GetComponent<Tape>());
        }
    }

    public void ClearField()
    {
        foreach(var t in tapes)
        {
            Destroy(t.gameObject);
        }
        tapes.Clear();
        preferredXPos.Clear();
    }

    public void SetNumber(Tape t)
    {
        Transform number = t.number.transform;
        number.GetComponent<Number>().boarded = true;

        buffer = number.localScale.x/2 + 0.1f;

        float pos = number.localPosition.x;

        if (preferredXPos.Count == 0) AddColumn(t);
        else
        {
            float closest = preferredXPos[0];
            for (int i = 0; i < preferredXPos.Count; i++)
            {
                if (Mathf.Abs(preferredXPos[i] - pos) < Mathf.Abs(closest - pos))
                {
                    closest = preferredXPos[i];
                }
            }
            if (Mathf.Abs(closest - pos) < buffer)
            {
                ChangeNumberOnPos(t, closest);

                Vector2 newNumPos = new(closest, 0);
                number.localPosition = newNumPos;
                number.SetSiblingIndex(preferredXPos.IndexOf(closest));

                SortColumns();
            }
            else
            {
                AddColumn(t);
            }
        }
        CheckBonus(t);
    }
    private void CheckBonus(Tape t)
    {
        float num = t.number.transform.localPosition.x;
        Bonus[] bs = t.transform.GetComponentsInChildren<Bonus>();
        if (bs.Length > 0)
        {
            for(int i = 0; i < bs.Length; i++)
            {
                float bPos = bs[i].transform.localPosition.x;
                if (Mathf.Abs(bPos - num) < buffer / 2)
                {
                    C.main.BonusUp(bs[i].type);
                    Destroy(bs[i].gameObject);
                    break;
                }
            }
        }
    }
    private void AddColumn(Tape t)
    {
        float x = t.number.transform.localPosition.x;
        preferredXPos.Add(x);
        preferredXPos.Sort();

        for (int i = 0; i < tapes.Count; i++)
        {
            if (tapes[i] == t) continue;

            Transform zero = new GameObject(0.ToString()).transform;
            zero.SetParent(tapes[i].transform);
            zero.localPosition = new(x, 0);
            zero.localScale = new(t.number.transform.localScale.x, 1);
            zero.SetSiblingIndex(preferredXPos.IndexOf(x));
            zero.AddComponent<SpriteRenderer>().sortingOrder = 1;
        }
        SortColumns();
    }
    private void SortColumns()
    {
        for (int i = 0; i < tapes.Count; i++)
        {
            for(int j = 0; j < preferredXPos.Count; j++)
            {
                Transform child = tapes[i].transform.GetChild(j);
                child.SetSiblingIndex(preferredXPos.IndexOf(child.transform.localPosition.x));
            }
        }
    }
    private void ChangeNumberOnPos(Tape t, float xPos)
    {
        Destroy(t.transform.GetChild(preferredXPos.IndexOf(xPos)).gameObject);
    }
    public int[,] CreateMatrix()
    {
        Bonus[] bs = FindObjectsOfType<Bonus>();
        if (bs.Length != 0)
        {
            foreach (var b in bs) Destroy(b.gameObject);
            SortColumns();
        }

        if (preferredXPos.Count == 0)
        {
            Debug.Log("There are no saved numbers on field!");
            
            return null;
        }

        numberGOs = new GameObject[tapes.Count, preferredXPos.Count];
        int[,] matrix = new int[tapes.Count, preferredXPos.Count];

        for(int i = 0; i < tapes.Count; i++)
        {
            tapes[i].StopTape();
            for(int j = 0; j< preferredXPos.Count; j++)
            {
                numberGOs[i, j] = tapes[i].transform.GetChild(j).gameObject;
                matrix[i, j] = Convert.ToInt32(numberGOs[i,j].name);

                if (numberGOs[i,j].name == 0.ToString())
                {
                    SpriteRenderer spr = tapes[i].transform.GetChild(j).GetComponent<SpriteRenderer>();
                    Sprite[] s = Resources.LoadAll<Sprite>("Sprites/numbers");
                    spr.sprite = s[0];
                }
                //Debug.Log($"{i}'s row {j} column {matrix[i, j]} value");
            }
        }
        return matrix;
    }
    public void HighlightNumbers(int x, int y)
    {
        SpriteRenderer rend = numberGOs[x,y].GetComponent<SpriteRenderer>();
        Sprite s = rend.sprite;

        GameObject h = new("H");
        h.transform.SetParent(numberGOs[x, y].transform);
        h.transform.localScale *= 1.2f;
        h.transform.localPosition = Vector3.zero;
        SpriteRenderer hRend = h.AddComponent<SpriteRenderer>();
        hRend.sprite = s;

        Color randColor = new(UnityEngine.Random.Range(0f, 1f)
            , UnityEngine.Random.Range(0f, 1f)
            , UnityEngine.Random.Range(0f, 1f));

        hRend.color = randColor;     
    }
}

