using Architecture;
using System;
using UnityEngine;

public class TimerInteractor : Interactor
{
    public event Action<float> OnTimerValueChangedEvent;
    public event Action OnTimerFinishedEvent;

    public float remainingSeconds
    {
        get => repository.leftTime;
        set => repository.leftTime = value;
    }

    public bool isPaused { get; private set; }
    public float roundTime => repository.originRoundTime;

    private StateMachine mainStateMachine;
    private TimerRepository repository;

    public override void OnCreate()
    {
        this.repository = Game.GetRepository<TimerRepository>();    

        
        mainStateMachine = C.main.MainStateMachine;
        if(mainStateMachine != null)
        {
            mainStateMachine.stateChanged += OnStateChanged;
        }
    }

    public void OnStateChanged(IState state)
    {
        if (state == mainStateMachine.playState)
        {
            Start(roundTime);
        }
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
        remainingSeconds = 0;

        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
        OnTimerFinishedEvent?.Invoke();

        mainStateMachine.TransitionTo(mainStateMachine.countState);
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
        }
        else
        {
            OnTimerValueChangedEvent?.Invoke(remainingSeconds);
        }
    }
}
