using Architecture;
using UnityEngine;
using UnityEngine.UI;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private Image timerFill;

    private TimerInteractor timerInteractor;

    private void Start()
    {
        timerInteractor = Game.GetInteractor<TimerInteractor>();
        if(timerInteractor != null)
        {
            timerInteractor.OnTimerValueChangedEvent += OnTimerValueChanged;
        }
    }
    private void OnDestroy()
    {
        if (timerInteractor != null)
        {
            timerInteractor.OnTimerValueChangedEvent -= OnTimerValueChanged;
        }
    }
    private void OnTimerValueChanged(float remainingSeconds)
    {
        timerFill.fillAmount = remainingSeconds / timerInteractor.roundTime;
    }
}
