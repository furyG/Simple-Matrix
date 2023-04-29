using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointsHandler
{
    private PointsPresenter pointsPresenter;
    private Transform canvasTransform;
    private Vector2 flyingEndPosition;
    private float pointsFlyingTime;

    public FloatingPointsHandler(PointsPresenter pointsPresenter, Transform canvasTransform, Vector2 flyingEndPosition)
    {
        this.pointsPresenter = pointsPresenter;
        this.canvasTransform = canvasTransform;
        this.flyingEndPosition = flyingEndPosition;

        this.pointsFlyingTime = Balance.GetInstance().PointsFlyingTime;
    }
    public void InitFloatingPoints(Vector2 startPos)
    {
        //Debug.Log("Spawning floating points on: " + num.Number);
        List<Vector2> fsPts;
        FloatingPoints fp;
        Vector2 p0 = startPos;
        //p0 = Camera.main.WorldToScreenPoint(p0);
        p0.x /= Screen.width;
        p0.y /= Screen.height;

        Vector2 p1 = flyingEndPosition;
        p1.x /= Screen.width;
        p1.y /= Screen.height;

        fsPts = new List<Vector2>
        {
            p0,p1
        };
        fp = CreateFloatingPoints(3, fsPts);
        fp.fontSizes = new List<float> { 4, 50, 28 };
        fp.reportFinishTo = pointsPresenter;
    }
    private FloatingPoints CreateFloatingPoints(int amt, List<Vector2> pts)
    {
        FloatingPoints fp = TapeObjectsFactory.GetInstance().Get<FloatingPoints>(canvasTransform);
        fp.points = amt;
        fp.reportFinishTo = pointsPresenter;
        fp.Init(pts, 0, pointsFlyingTime);
        return fp;
    }
}
