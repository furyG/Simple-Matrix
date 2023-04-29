using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsPresenter : MonoBehaviour
{
    [SerializeField] private Image pointsImage;
    [SerializeField] private TextMeshProUGUI pointsTxt;
    [SerializeField] private Transform canvasTransform;

    private Image activePointsFill;
    private PointsInteractor pointsInteractor;
    private FloatingPointsHandler floatingPointsHandler;

    private void Start()
    {
        pointsInteractor = Game.GetInteractor<PointsInteractor>();

        if(pointsInteractor != null)
        {
            pointsInteractor.pointsChanged += OnPointsChangedEvent;
            pointsInteractor.comboRecieved += OnComboRecievedEvent;
        }

        floatingPointsHandler = new FloatingPointsHandler(this, canvasTransform, pointsImage.transform.position);
    }

    private void OnDestroy()
    {
        if (pointsInteractor != null)
        {
            pointsInteractor.pointsChanged -= OnPointsChangedEvent;
            pointsInteractor.comboRecieved -= OnComboRecievedEvent;
        }
    }
    private void OnComboRecievedEvent(List<Vector2> numsToAnimate)
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
            pointsTxt.text = "счёт: " + pointsInteractor.points.ToString();
        }
    }
    private void OnPointsChangedEvent()
    {
        UpdateView();
    }
    public void FPCallback(FloatingPoints floatingPoints)
    {
        //delete
    }
}
