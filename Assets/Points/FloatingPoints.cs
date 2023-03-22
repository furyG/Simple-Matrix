using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public enum FPState
{
    idle,
    pre,
    active,
    post
}

public class FloatingPoints : SpawnableObject
{
    private FPState state = FPState.idle;

    protected int _points = 0;
    private string scoreString;

    public int points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            scoreString = _points.ToString("N0");
            txt.text = scoreString;
        }
    }

    public override SpawnableType Type => SpawnableType.Points;
    public List<float> fontSizes;
    public PointsInteractor reportFinishTo = null;

    private List<Vector2> bezierPts;

    private float timeStart = -1f;
    private float timeDuration = 1f;
    private string easingCurve = Easing.InOut;

    private RectTransform rectTrans;
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
        rectTrans = GetComponent<RectTransform>();
    }

    public void Init(List<Vector2> ePts, float eTimeS = 0, float eTimeD = 1)
    {
        rectTrans.anchoredPosition = Vector2.zero;

        bezierPts = new List<Vector2>(ePts);

        if(ePts.Count == 1)
        {
            transform.position = ePts[0];
            return;
        }
        if (eTimeS == 0) eTimeS = Time.time;
        timeStart= eTimeS;
        timeDuration = eTimeD;

        state = FPState.pre;
    }
    private void Update()
    {
        if (state == FPState.idle) return;

        float u = (Time.time - timeStart) / timeDuration;
        float uC = Easing.Ease(u, easingCurve);
        if (u < 0)
        {
            state = FPState.pre;
            txt.enabled = false;
        }
        else
        {
            if(u >= 1)
            {
                uC = 1;
                state = FPState.post;

                reportFinishTo.FPCallback(this);
                Destroy(gameObject);
            }
            else
            {
                state = FPState.active;
                txt.enabled = true;
            }

            Vector2 pos = Utils.Bezier(uC, bezierPts);
            rectTrans.anchorMin = rectTrans.anchorMax = pos;
            if(fontSizes != null && fontSizes.Count > 0)
            {
                int size = Mathf.RoundToInt(Utils.Bezier(uC, fontSizes));
                txt.fontSize = size;
            }
        }
    }
    public void FPCallback(FloatingPoints fp)
    {
        points += fp.points;
    }
}
