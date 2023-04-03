using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    T Get<T>(Transform parent = null) where T : Component;
}
