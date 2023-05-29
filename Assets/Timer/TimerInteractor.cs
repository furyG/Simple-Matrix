using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerInteractor : Interactor, IBonusReciever
{
    public event Action<float> OnTimerValueChangedEvent;
    public event Action OnTimerFinishedEvent;

    public float remainingSeconds
    {
        get => repository.leftTime;
        set => repository.leftTime = value;
    }

    public bool isPaused { get; private set; }
    public float roundTime => repository.originLevelTime;

    private TimerRepository repository;
    private PointsInteractor pointsInteractor;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<TimerRepository>();    
        
        pointsInteractor = Game.GetInteractor<PointsInteractor>();

        if(pointsInteractor != null)
        {
            pointsInteractor.comboRecieved += OnComboRecieved;
        }
    }

    private void OnComboRecieved(List<Vector2> numsPoses)
    {
        float additionTime = (numsPoses.Count * repository.timerIncrementForPoint) + (numsPoses.Count/2);
        AddTime(null, additionTime);
    }

    public void SetTime(float seconds)
    {
        remainingSeconds = seconds;
        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }

    public void Start()
    {
        if(remainingSeconds == 0)
        {
            Debug.LogError("Timer: u are trying to start ended timer;");
            OnTimerFinishedEvent?.Invoke();
        }
        isPaused = false;
        Subscribe();
        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }
    public void Start(float seconds)
    {
        SetTime(seconds);
        Start();
    }
    public void StartRoundTimer()
    {
        SetTime(roundTime);
        Start();
    }
    public void Pause()
    {
        isPaused = true;
        Unsubscribe();
        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }
    public void Unpause()
    {
        isPaused = false;
        Subscribe();
        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }
    public void Stop()
    {
        Unsubscribe();
        //remainingSeconds = 0;

        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
        OnTimerFinishedEvent?.Invoke();
    }
    private void Subscribe()
    {
        TimerInvoker.instance.OnUpdateTimeTickedEvent += OnTick;
    }
    private void Unsubscribe()
    {
        TimerInvoker.instance.OnUpdateTimeTickedEvent -= OnTick;
    }
    private void OnTick(float delta)
    {
        if (isPaused) return;
        remainingSeconds -= delta;

        CheckFinish();
    }
    private void CheckFinish()
    {
        if(remainingSeconds <= 0)
        {
            Stop();
            GameModeManager.GetInstance().GameOver(true);
        }
        else
        {
            OnTimerValueChangedEvent?.Invoke(remainingSeconds);
        }
    }
    private void AddTime(object sender, float amount)
    {
        remainingSeconds += amount;
        remainingSeconds = Mathf.Clamp(remainingSeconds, 0, repository.originLevelTime);
    }

    public void TakeBonus()
    {
        AddTime(null, repository.timerBonusIncrementAmount);
    }
}
