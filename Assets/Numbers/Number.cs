using System.Collections;
using UnityEngine;

public class Number : Numberable
{
    public override SpawnableType Type => SpawnableType.Number;

    private enum numberType
    {
        simple,
        allNums,
        changing
    }
    private float runTime;
    private numberType type = numberType.simple;

    protected override void Awake()
    {
        base.Awake();

        runTime = Balance.instance.NumbersRunTime;
    }

    protected override void Start()
    {
        base.Start();

        StartCoroutine(NumberMoveRoutine());
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
            if (chance > 0.9f)
            {
                type = numberType.allNums;
            }
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
            case numberType.allNums:
                Number = 10;
                break;
        }
        
    }
    private int GetRandomNum() => Random.Range(1, 8);

    private IEnumerator NumberMoveRoutine()
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
}
