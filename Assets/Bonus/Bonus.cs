using System.Collections;
using System.Collections.Generic;
using Tapes;
using UnityEngine;

    public enum bType
    {
        time = 0,
        slow = 1
    }

public class Bonus : Tile
{
    public override SpawnableType Type => SpawnableType.Bonus;

    private int lifeTime = 5;
    private bType type = bType.time;

    private float buffer = 0;

    protected override void Start()
    {
        base.Start();

        buffer = scale.x / 2 + 0.8f;
    }

    protected override void SetStartPos()
    {
        float xPos = Random.Range(-0.3f, 0.3f);
        if (parent.childCount > 0)
        {
            int index = Random.Range(0, transform.childCount);
            xPos = parent.GetChild(index).localPosition.x;
            if (parent.GetChild(index).name != 0.ToString())
            {
                xPos -= buffer * Mathf.Sign(xPos);
            }
        }
        transform.localPosition = new(xPos, 0, 0);
    }
    protected override void SetType()
    {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance > 0.5f) type = bType.time;
        else type = bType.slow;
    }

    protected override void InvokeTapeTile()
    {
        Sprite[] bSprites = Resources.LoadAll<Sprite>("Sprites/bonuses");
        rend.sprite = bSprites[(int)type];

        Destroy(gameObject, lifeTime);
    }
    public override void Interact() => C.main.BonusUp(type);
}
