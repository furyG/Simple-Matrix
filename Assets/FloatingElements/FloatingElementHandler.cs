using System.Collections.Generic;
using UnityEngine;

public class FloatingElementHandler<T> where T : FloatingElement
{
    private IFloatingElementReportable _reportFinishTo { get; set; }
    private Vector2 flyingEndPosition;
    private float pointsFlyingTime = 1.5f;

    private PoolMono<T> _pool;

    public FloatingElementHandler(IFloatingElementReportable reportFinishTo, Vector2 flyingEndPosition, PoolMono<T> pool)
    {
        this._reportFinishTo = reportFinishTo;
        this.flyingEndPosition = flyingEndPosition;

        this._pool = pool;
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
        floatingElement = GetFloatingElement(fsPts);
        floatingElement.fontSizes = new List<float> { 4, 50, 28 };
        floatingElement.reportFinishTo = _reportFinishTo;
    }
    public T GetFloatingElement(List<Vector2> pts)
    {
        T floatingElement = _pool.GetFreeElement();
        floatingElement.reportFinishTo = _reportFinishTo;
        floatingElement.Init(pts, 0, pointsFlyingTime);

        return floatingElement;
    }
}
