using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class Number : MonoBehaviour
{
    public bool boarded = false;

    private enum nType
    {
        simple,
        allNums,
        changing
    }
    private nType type = nType.simple;
    private int number = 0;

    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        InvokeNumber();
    }

    private void InvokeNumber()
    {
        float chance = Random.Range(0f, 1f);
        if(chance > 0.8f)
        {
            type = nType.changing;
            if(chance > 0.9f)
            {
                type = nType.allNums;
            }
        }
        ChangeNumValue();
    }
    private void ChangeNumValue()
    {
        if (boarded) return;
        switch (type)
        {
            case nType.simple:
                number = GetRandomNum();
                break;
            case nType.changing:
                Invoke(nameof(ChangeNumValue), 0.8f);
                Mathf.Clamp(number++, 0, 7);
                break;
            case nType.allNums:
                number = 10;
                break;
        }
        Sprite[] s = Resources.LoadAll<Sprite>("Sprites/numbers");
        rend.sprite = s[number];
        name = number.ToString();
    }
    private int GetRandomNum() => Random.Range(1, 8);
}
