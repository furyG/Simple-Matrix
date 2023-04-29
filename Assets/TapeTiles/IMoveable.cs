using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable<T> where T : Enum
{
    event Action OnMovingEnd;
    float runDuration { get; set; }
    float slowRunDuration { get; }
    float defaultRunDuration { get; }
    void StartMove();
    void EndMove();

    bool CheckForSlow();
    void SlowUp();
    void SlowEnd();
    IEnumerator MoveCoroutine();
    IEnumerator moveCoroutine { get; set; }
}
