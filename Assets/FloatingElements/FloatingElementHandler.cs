using Architecture;
using System.Collections.Generic;
using UnityEngine;

public class FloatingElementHandler<T> where T : FloatingElement
{
    private IFinishReportable _reportFinishTo { get; set; }
    private Transform canvasTransform;
    private Vector2 flyingEndPosition;
    private PointsSettingsConfig _pointsSettingsConfig;
    private float pointsFlyingTime;

    public FloatingElementHandler(IFinishReportable reportFinishTo, Transform canvasTransform, Vector2 flyingEndPosition)
    {
        this._reportFinishTo = reportFinishTo;
        this.canvasTransform = canvasTransform;
        this.flyingEndPosition = flyingEndPosition;

        _pointsSettingsConfig = Game.GetInteractor<ConfigInteractor>().GetConfig<PointsSettingsConfig>();
        this.pointsFlyingTime = _pointsSettingsConfig.pointsFlyingTime;
    }
    public void InitFloatingElement(Vector2 startPos)
    {
        List<Vector2> fsPts;
        T floatingElement;
        Vector2 p0 = startPos;
        p0.x /= Screen.width;
        p0.y /= Screen.height;

        Vector2 p1 = flyingEndPosition;
        p1.x /= Screen.width;
        p1.y /= Screen.height;

        fsPts = new List<Vector2>
        {
            p0,p1
        };
        floatingElement = CreateFloatingElement(3, fsPts);
        floatingElement.fontSizes = new List<float> { 4, 50, 28 };
        floatingElement.reportFinishTo = _reportFinishTo;
    }
    public T CreateFloatingElement(int amt, List<Vector2> pts)
    {
        T floatingElement = TapeObjectsFactory.GetInstance().Get<T>(canvasTransform);
        floatingElement.reportFinishTo = _reportFinishTo;
        floatingElement.Init(pts, 0, pointsFlyingTime);

        if(typeof(T) == typeof(FloatingPoints))
        {
            FloatingPoints floatingPoints = floatingElement as FloatingPoints;
            floatingPoints.points = amt;
        }
        if(typeof(T) == typeof(FloatingHearts)) 
        {
            FloatingHearts floatingHearts = floatingElement as FloatingHearts;
            floatingHearts.ChangeScale();
        }

        return floatingElement;
    }
}
