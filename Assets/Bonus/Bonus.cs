using System.Collections;
using System.Collections.Generic;
using Tapes;
using UnityEngine;

    public enum bType
    {
        time = 0,
        slow = 1
    }

public class Bonus : TapeTile
{
    public int lifeTime = 5;

    private bType type = bType.time;

    protected override void Awake()
    {
        base.Awake();
        SetStartPos();
    }
    private void SetStartPos()
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

    private void Start()
    {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance > 0.5f) type = bType.time;
        else type = bType.slow;

        InvokeBonus();
    }

    private void InvokeBonus()
    {
        Sprite[] bSprites = Resources.LoadAll<Sprite>("Sprites/bonuses");
        rend.sprite = bSprites[(int)type];

        Destroy(gameObject, lifeTime);
    }
    public override void Interact() => C.main.BonusUp(type);
}
