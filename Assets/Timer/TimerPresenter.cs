using Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private Image timerFill;
    [SerializeField] private TextMeshProUGUI addedTimeText;

    private TimerInteractor timerInteractor;

    private void Start()
    {
        timerInteractor = Game.GetInteractor<TimerInteractor>();
        if(timerInteractor != null)
        {
            timerInteractor.OnTimerValueChangedEvent += OnTimerValueChanged;
            timerInteractor.OnTimerFinishedEvent += OnTimerFinished;
        }
    }
    private void OnDestroy()
    {
        if (timerInteractor != null)
        {
            timerInteractor.OnTimerValueChangedEvent -= OnTimerValueChanged;
            timerInteractor.OnTimerFinishedEvent -= OnTimerFinished;
        }
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
}
