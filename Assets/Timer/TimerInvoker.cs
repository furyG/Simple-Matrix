using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerInvoker : MonoBehaviour
{
    public event Action<float> OnUpdateTimeTickedEvent;

    public static TimerInvoker instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("[TIME INVOKER]");
                _instance = go.AddComponent<TimerInvoker>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    private static TimerInvoker _instance;

    private void Update()
    {
        var delta = Time.deltaTime;
        OnUpdateTimeTickedEvent?.Invoke(delta);
    }
}
