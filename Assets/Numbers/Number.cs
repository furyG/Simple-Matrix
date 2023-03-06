using System.Collections;
using Tapes;
using UnityEngine;

public class Number : TapeTile 
{
    public bool boarded = false;
    private float runTime;

    private enum nType
    {
        simple,
        allNums,
        changing
    }
    private nType type = nType.simple;
    private int number = 0;

    protected override void Awake()
    {
        base.Awake();

        float edge = Random.Range(-0.1f, 0.1f);
        float lPosX = (0.5f - scale.x / 2) * Mathf.Sign(edge);
        transform.localPosition = new(lPosX, 0f);

        runTime = Balance.instance.NumbersRunTime;
    }

    private void Start()
    {
        InvokeNumber();
        StartCoroutine(NumberMove());
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

    private IEnumerator NumberMove()
    {
        float startX = transform.localPosition.x;

        float elapsedTime = 0f;
        while (elapsedTime < runTime)
        {
            elapsedTime += Time.deltaTime;
            float xPos = Mathf.Lerp(startX, startX * -1, elapsedTime / (runTime));
            transform.localPosition = new(xPos, 0f);
            yield return null;
        }
        Destroy(gameObject);
    }
    public override void Interact()
    {
        TapeHandler.instance.SetNumber(this);
        StopAllCoroutines();
    }
}
