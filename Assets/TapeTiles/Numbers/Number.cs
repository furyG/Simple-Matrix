using System.Collections;
using Tapes;
using UnityEngine;

public class Number : Numberable, ISlowable
{
    private enum numberType
    {
        simple,
        //allNums,
        changing
    }
    private float numberRunDuration;
    private numberType type = numberType.simple;
    private IEnumerator numberRunCoroutine;

    private Balance balance = Balance.instance;

    protected override void Start()
    {
        base.Start();

        if (balance)
        {
            balance.slowBonusTaken += SlowUp;
            balance.slowBonusDurationEnd += SlowEnd;
        }

        numberRunDuration = CheckForSlow() ? balance.SlowNumberRunDuration : balance.NumberRunDuration;
        numberRunCoroutine = NumberMoveCoroutine();

        StartCoroutine(numberRunCoroutine);
    }

    protected override void OnDestroy()
    {
        if (balance)
        {
            balance.slowBonusTaken -= SlowUp;
            balance.slowBonusDurationEnd -= SlowEnd;
        }
    }

    protected override void SetStartPos()
    {
        float edge = Random.Range(-0.1f, 0.1f);
        float lPosX = (0.5f - scale.x / 2) * Mathf.Sign(edge);
        transform.localPosition = new(lPosX, 0f);
    }
    protected override void SetType()
    {
        float chance = Random.Range(0f, 1f);
        if (chance > 0.8f)
        {
            type = numberType.changing;
            //if (chance > 0.9f)
            //{
            //    type = numberType.allNums;
            //}
        }
    }

    protected override void InvokeTapeTile()
    {
        if (boarded) return;
        switch (type)
        {
            case numberType.simple:
                Number = GetRandomNum();
                break;
            case numberType.changing:
                Mathf.Clamp(Number++, 0, 7);
                Invoke(nameof(InvokeTapeTile), 0.8f);
                break;
            //case numberType.allNums:
            //    Number = 10;
            //    break;
        }
        
    }
    private int GetRandomNum() => Random.Range(1, 7);

    private IEnumerator NumberMoveCoroutine()
    {
        float startX = transform.localPosition.x;

        float elapsedTime = 0f;
        while (elapsedTime < numberRunDuration)
        {
            elapsedTime += Time.deltaTime;
            float xPos = Mathf.Lerp(startX, startX * -1, elapsedTime / numberRunDuration);
            transform.localPosition = new(xPos, 0f);
            yield return null;
        }
        Destroy(gameObject);
    }
    public override void SetNumberable()
    {
        base.SetNumberable();
        StopCoroutine(numberRunCoroutine);
    }
    public bool CheckForSlow()
    {
        return balance.Slowed;
    }

    public void SlowEnd()
    {
        numberRunDuration = balance.NumberRunDuration;
    }

    public void SlowUp()
    {
        numberRunDuration = balance.SlowNumberRunDuration;
    }
}
