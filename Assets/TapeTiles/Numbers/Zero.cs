using System.Collections;
using System.Collections.Generic;
using Tapes;
using UnityEngine;

public class Zero : Numberable
{
    protected override void Awake()
    {
        base.Awake();

        Number = 0;
        boarded = true;
    }

    protected override void InvokeTapeTile()
    {
        Sprite[] s = Resources.LoadAll<Sprite>("Sprites/numbers");
        rend.sprite = s[0];
    }

    protected override void SetStartPos()
    {
        
    }

    protected override void SetType()
    {
        
    }
}
