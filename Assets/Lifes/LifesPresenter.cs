using Architecture;
using System.Collections.Generic;
using UnityEngine;

public class LifesPresenter : MonoBehaviour, IFloatingElementReportable
{
    [SerializeField] private Transform _heartPointsTarget;
    [SerializeField] private Transform _lifesContainer;
    [SerializeField] private Transform _heartPoolContainer;

    [SerializeField] private int heartPoolLength;
    [SerializeField] private bool _autoExpand = false;

    private LifesInteractor _lifesInteractor;
    private FloatingElementHandler<FloatingHearts> _floatingHeartsHandler;

    private PoolMono<FloatingHearts> _heartPool;

    private void Awake()
    {
        _lifesInteractor = Game.GetInteractor<LifesInteractor>();

        _heartPool = new PoolMono<FloatingHearts>(heartPoolLength, _heartPoolContainer);
        _heartPool.autoExpand = _autoExpand;
        _floatingHeartsHandler = new FloatingElementHandler<FloatingHearts>(this, _heartPointsTarget.transform.position, _heartPool);

        if (_lifesInteractor != null)
        {
            _lifesInteractor.heartScoreChanged += UpdateView;
            _lifesInteractor.comboRecieved += OnComboRecievedEvent;
        }
    }

    private void OnComboRecievedEvent(List<Vector2> numsPositions)
    {
        foreach(var numPos in numsPositions)
        {
            _floatingHeartsHandler.InitFloatingElement(numPos);
        }
    }

    private void OnDestroy()
    {
        if(_lifesInteractor != null)
        {
            _lifesInteractor.heartScoreChanged -= UpdateView;
            _lifesInteractor.comboRecieved -= OnComboRecievedEvent;
        }
    }

    private void UpdateView(float heartScore)
    {
        float remainder = heartScore;

        for (int i = 0; i < _lifesContainer.childCount; i++)
        {
            HeartPresenter heart = _lifesContainer.GetChild(i).GetComponent<HeartPresenter>();
            heart.ResetFillAmount();

            float needFillAmount = 1 - heart.GetFillAmount();
            heart.ChangeFillAmount(remainder);
            remainder -= needFillAmount;
            remainder = Mathf.Clamp(remainder, 0, remainder);
        }
    }

    public void FloatingElementCallBack()
    {
        Debug.Log("points home");
    }
}
