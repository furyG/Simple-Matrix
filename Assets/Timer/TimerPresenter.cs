using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private Level lvl;
    [SerializeField] private Timer timer;
    [SerializeField] private Image timerFill;

    private void Start()
    {
        if (timer != null) timer.TimeChanged += OnTimeChanged;
        if (lvl != null) lvl.LevelChanged += ChangeTimer;
        UpdateView();
    }
    private void OnDestroy()
    {
        if (timer != null) timer.TimeChanged -= OnTimeChanged;
        if (lvl != null) lvl.LevelChanged -= ChangeTimer;
    }

    public void ChangeTimer(float lvlStartTime)
    {
        timer?.ChangeTimer(lvlStartTime);
    }
    public void IncreaseRoundTime()
    {
        timer?.IncreaseRoundTime();
    }
    private void UpdateView()
    {
        if (timer == null) return;
        if (timerFill != null && timer.LeftTime != 0)
        {
            timerFill.fillAmount = timer.LeftTime / timer.RoundTime;
        }
    }
    private IEnumerator ChangeTimerFill()
    {
        while(timer.LeftTime > 0)
        {
            timer.LeftTime -= 1 * Time.deltaTime;
            UpdateView();
            yield return null;
        }

    }
    private void OnTimeChanged()
    {
        StartCoroutine(ChangeTimerFill());
    }
}
