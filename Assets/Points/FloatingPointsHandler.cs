using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointsHandler
{
    private PointsInteractor pointsInteractor;
    private Transform canvasTransform;
    private Vector2 flyingEndPosition;

    public FloatingPointsHandler(PointsInteractor pointsInteractor, Transform canvasTransform, Vector2 flyingEndPosition)
    {
        this.pointsInteractor = pointsInteractor;
        this.canvasTransform = canvasTransform;
        this.flyingEndPosition = flyingEndPosition;
    }
    public void InitFloatingPoints(Numberable num)
    {
        //Debug.Log("Spawning floating points on: " + num.Number);
        List<Vector2> fsPts;
        FloatingPoints fp;
        Vector2 p0 = num.transform.position;
        p0 = Camera.main.WorldToScreenPoint(p0);
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
        fp.reportFinishTo = pointsInteractor;
    }
    private FloatingPoints CreateFloatingPoints(int amt, List<Vector2> pts)
    {
        GameObject go = TapeContentFabric.instance.GetAndSetParent(SpawnableType.Points, canvasTransform).gameObject;
        FloatingPoints fp = go.GetComponent<FloatingPoints>();
        fp.points = amt;
        fp.reportFinishTo = pointsInteractor;
        fp.Init(pts, 0, 3);
        return fp;
    }
}
