using System;
using System.Collections;

public interface IMoveable
{
    event Action OnMovingEnd;
    float runDuration { get; set; }
    float defaultRunDuration { get; }
    void StartMove();
    void EndMove();
    IEnumerator MoveCoroutine();
}
