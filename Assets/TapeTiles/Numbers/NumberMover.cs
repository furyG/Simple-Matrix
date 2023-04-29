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

    private float runElapsedTime;
    private float startRunningPos;

    protected Transform _parent;
    protected RectTransform _rectTransform;
    private Balance balance;
    private NumberManager _manager;

    private void Awake()
    {
        _manager = GetComponent<NumberManager>();
        _rectTransform = GetComponent<RectTransform>();
        

        balance = Balance.GetInstance();

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
    private void OnEnable()
    {
        StartMove();
    }
    private void OnDisable()
    {
        EndMove();
    }
    public void EndMove()
    {
        if (moveCoroutine != null)
        { 
            StopCoroutine(MoveCoroutine());
            runElapsedTime = 0f;
        }
    }
    public void StartMove()
    {
        SetStartPosition();
        startRunningPos = transform.localPosition.x;

        StartCoroutine(MoveCoroutine());
    }

    public IEnumerator MoveCoroutine()
    {
        runElapsedTime = 0f;
        while (runElapsedTime < runDuration)
        {
            runElapsedTime += Time.deltaTime;
            float xPos = Mathf.Lerp(startRunningPos, startRunningPos * -1, runElapsedTime / runDuration);
            transform.localPosition = new(xPos, 0f);
            yield return null;
        }

        OnMovingEnd?.Invoke();
        gameObject.SetActive(false);
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


    public void SetStartPosition(Vector2 startPos = default)
    {
        if (startPos != default)
        {
            transform.localPosition = startPos;
            return;
        }

        float edge = UnityEngine.Random.Range(-0.1f, 0.1f);

        float parentWidth = transform.parent.GetComponent<RectTransform>().rect.width;
        float lPosX = (parentWidth / 2 - _rectTransform.rect.width / 2) * Mathf.Sign(edge);

        transform.localPosition = new(lPosX, 0f);
    }
}

