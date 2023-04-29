using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Timer
{
    public static float remainingSeconds => timerInteractor.remainingSeconds;
    public static bool isPaused => timerInteractor.isPaused;

    private static TimerInteractor timerInteractor;

    public static void Initialize(TimerInteractor interactor)
    {
        timerInteractor = interactor;
    }
    public static void SetTime(float seconds) => timerInteractor.SetTime(seconds);

    public static void Start() => timerInteractor.Start();

    public static void Start(float seconds) => timerInteractor.Start(seconds);

    public static void StartRoundTimer() => timerInteractor.StartRoundTimer();

    public static void Pause() => timerInteractor.Pause();

    public static void Unpause() => timerInteractor.Unpause();

    public static void Stop() => timerInteractor.Stop();
}
