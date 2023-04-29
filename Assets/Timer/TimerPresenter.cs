using UnityEngine;
using UnityEngine.UI;
using Architecture;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private Image timerFill;

    private TimerInteractor timerInteractor;

    private void Start()
    {
        timerInteractor = Game.GetInteractor<TimerInteractor>();
        timerInteractor.OnTimerValueChangedEvent += OnTimerValueChanged;
        timerInteractor.OnTimerFinishedEvent += OnTimerFinished;
    }
    private void OnDestroy()
    {
        timerInteractor.OnTimerValueChangedEvent -= OnTimerValueChanged;
        timerInteractor.OnTimerFinishedEvent -= OnTimerFinished;
    }
    private void OnTimerFinished()
    {

    }
    private void OnTimerValueChanged(float remainingSeconds)
    {
        timerFill.fillAmount = remainingSeconds / timerInteractor.roundTime;
    }

    private void StartTimerClicked()
    {
        timerInteractor.Start();
    }
    private void PauseTimerClicked()
    {
        if (timerInteractor.isPaused) timerInteractor.Unpause();
        else timerInteractor.Pause();
    }
    private void StopTimerClicked()
    {

    }

}
