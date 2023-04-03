using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver : IDisposable
{
    void AddObservable(IObservable observable);
}
