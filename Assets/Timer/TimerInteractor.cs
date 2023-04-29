using Architecture;
using System;
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
        Timer.Initialize(this);
    }

    public override void Initialize()
    {
        if (pointsInteractor != null)
        {
            pointsInteractor.pointsForLevelUpCollected += AddLevelUpTime;
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
            GameModeManager.GetInstance().GameOver();
        }
        else
        {
            OnTimerValueChangedEvent?.Invoke(remainingSeconds);
        }
    }
    private void AddTime(object sender, float amount)
    {
        remainingSeconds += amount;
        remainingSeconds = Mathf.Clamp(remainingSeconds, 0, Balance.GetInstance().FirstLevelTime);
    }
    private void AddLevelUpTime()
    {
        AddTime(null, repository.levelUpIncrementTime);
    }

    public void TakeBonus()
    {
        AddTime(null, repository.timerBonusIncrementAmount);
    }
}
