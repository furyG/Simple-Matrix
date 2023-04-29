using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points 
{
    public static int points => pointsInteractor.points;
    public static int maxPointsForLevel => pointsInteractor.maxPointsOnLvl;

    private static PointsInteractor pointsInteractor;
    public static void Initialize(PointsInteractor interactor)
    {
        pointsInteractor = interactor;  
    }
    public static bool IsEnoughPoints() => pointsInteractor.IsEnoughPoints();

    public static void RecieveCombo(List<Vector2> numsToAnimate) => pointsInteractor.RecieveCombo(numsToAnimate);

    public static void AddPoints(object sender, int value) => pointsInteractor.AddPoints(sender, value);

    public static void LosePoints(object sender, int value) => pointsInteractor.LosePoints(sender, value);

    public static void Reset() => pointsInteractor.Reset();
}
