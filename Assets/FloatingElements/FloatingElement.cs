using System.Collections.Generic;
using UnityEngine;

public enum FPState
{
    idle,
    pre,
    active,
    post
}

public abstract class FloatingElement : MonoBehaviour
{
    public List<float> fontSizes { get; set; }
    public IFloatingElementReportable reportFinishTo = null;

    private List<Vector2> bezierPts;
    private FPState state = FPState.idle;
    private RectTransform rectTrans;

    private float timeStart = -1f;
    private float timeDuration = 1f;
    private readonly string easingCurve = Easing.InOut;

    public void Init(List<Vector2> ePts, float eTimeS = 0, float eTimeD = 1)
    {
        rectTrans = GetComponent<RectTransform>();
        rectTrans.anchoredPosition = Vector2.zero;

        bezierPts = new List<Vector2>(ePts);

        if (ePts.Count == 1)
        {
            transform.position = ePts[0];
            return;
        }
        if (eTimeS == 0) eTimeS = Time.time;
        timeStart = eTimeS;
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
            ToggleVizualizer(false);
        }
        else
        {
            if (u >= 1)
            {
                uC = 1;
                state = FPState.post;

                gameObject.SetActive(false);
            }
            else
            {
                state = FPState.active;
                ToggleVizualizer(true);
            }

            Vector2 pos = Utils.Bezier(uC, bezierPts);
            rectTrans.anchorMin = rectTrans.anchorMax = pos;
            if (fontSizes != null && fontSizes.Count > 0)
            {
                int size = Mathf.RoundToInt(Utils.Bezier(uC, fontSizes));
                ChangeFont(size);
            }
        }
    }
    public virtual void FPCallback(FloatingPoints fp) { }
    protected virtual void ToggleVizualizer(bool state) { }
    protected virtual void ChangeFont(int size) { }
    protected virtual void ChangeSprite(Sprite sprite) { }
}
