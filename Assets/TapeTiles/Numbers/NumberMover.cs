using System;
using System.Collections;
using UnityEngine;

public class NumberMover : MonoBehaviour, IMoveable<NumberType>, ISpawnable
{
    public event Action OnMovingEnd;

    public float runDuration { get; set; }
    public float slowRunDuration => balance.SlowNumberRunDuration;
    public IEnumerator moveCoroutine { get; set; }
    public float defaultRunDuration => balance.NumberRunDuration;

    protected Transform _parent;
    protected RectTransform _rectTransform;
    private Balance balance;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parent = transform.parent;

        balance = Balance.instance;

        runDuration = CheckForSlow() ? slowRunDuration : balance.NumberRunDuration;

        moveCoroutine = MoveCoroutine();
    }
    private void Start()
    {
        if (balance != null)
        {
            balance.slowBonusTaken += SlowUp;
            balance.slowBonusDurationEnd += SlowEnd;
        }
    }
    private void OnDestroy()
    {
        if (balance != null)
        {
            balance.slowBonusTaken -= SlowUp;
            balance.slowBonusDurationEnd -= SlowEnd;
        }
    }
    public void EndMove()
    {
        StopCoroutine(moveCoroutine);
    }

    public IEnumerator MoveCoroutine()
    {
        float startX = transform.localPosition.x;

        float elapsedTime = 0f;
        while (elapsedTime < runDuration)
        {
            elapsedTime += Time.deltaTime;
            float xPos = Mathf.Lerp(startX, startX * -1, elapsedTime / runDuration);
            transform.localPosition = new(xPos, 0f);
            yield return null;
        }

        Destroy(gameObject);
        OnMovingEnd?.Invoke();
    }
    public bool CheckForSlow()
    {
        return balance.Slowed;
    }

    public void SlowEnd()
    {
        runDuration = balance.NumberRunDuration;
    }

    public void SlowUp()
    {
        runDuration = balance.SlowNumberRunDuration;
    }

    public void InvokeMoving(Vector2 startPos = default)
    {
        if(startPos != default)
        {
            transform.localPosition = startPos;
            return;
        }

        SetStartPosition();

        StartCoroutine(moveCoroutine);
    }

    public void SetStartPosition()
    {
        float edge = UnityEngine.Random.Range(-0.1f, 0.1f);

        float parentWidth = _parent.GetComponent<RectTransform>().rect.width;
        float lPosX = (parentWidth / 2 - _rectTransform.rect.width / 2) * Mathf.Sign(edge);

        transform.localPosition = new(lPosX, 0f);
    }
}

