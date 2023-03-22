using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsPresenter : MonoBehaviour
{
    [SerializeField] private Image pointsImage;
    [SerializeField] private Text pointsTxt;
    [SerializeField] private Transform canvasTransform;

    private PointsInteractor pointsInteractor;
    private FloatingPointsHandler floatingPointsHandler;

    private void Start()
    {
        pointsInteractor = Game.GetInteractor<PointsInteractor>();
        if(pointsInteractor != null)
        {
            pointsInteractor.OnPointsChanged += OnPointsChangedEvent;
            pointsInteractor.OnComboRecieved += OnComboRecievedEvent;
        }

        floatingPointsHandler = new FloatingPointsHandler(pointsInteractor, canvasTransform, pointsTxt.transform.position);
    }


    private void OnDestroy()
    {
        if (pointsInteractor != null)
        {
            pointsInteractor.OnPointsChanged -= OnPointsChangedEvent;
            pointsInteractor.OnComboRecieved -= OnComboRecievedEvent;
        }
    }
    private void OnComboRecievedEvent(List<Numberable> numsToAnimate)
    {
        foreach(var num in numsToAnimate)
        {
            floatingPointsHandler.InitFloatingPoints(num);
        }
    }
    private void UpdateView()
    {
        if (pointsInteractor == null) return;

        if(pointsTxt != null && pointsImage != null)
        {
            pointsImage.fillAmount = (float)pointsInteractor.points / (float)pointsInteractor.maxPointsOnLvl;
            pointsTxt.text = pointsInteractor.points.ToString() + " of " + pointsInteractor.maxPointsOnLvl.ToString();
        }
    }
    private void OnPointsChangedEvent()
    {
        UpdateView();
    }
}
