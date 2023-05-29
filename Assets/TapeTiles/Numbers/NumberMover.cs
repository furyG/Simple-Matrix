using System;
using System.Collections;
using UnityEngine;

public class NumberMover : MonoBehaviour, IMoveable, ISpawnable
{
    public event Action OnMovingEnd;

    public float runDuration { get; set; }
    public float defaultRunDuration { get; }

    private float runElapsedTime;
    private float startRunningPos;

    private RectTransform _rectTransform;
    private NumberComplicationHandler _numberComplicationHandler;

    private void Awake()
    {
        _numberComplicationHandler = GetComponent<NumberManager>().numberComplicationHandler;
        _rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        runDuration = _numberComplicationHandler.GetRunDuration();
        StartMove();
    }
    private void OnDisable()
    {
        EndMove();
    }
    public void EndMove()
    {
        if (MoveCoroutine() != null)
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

