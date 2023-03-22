using System.Collections;
using System.Collections.Generic;
using Tapes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectFabric : MonoBehaviour
{
    protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
    {
        T instance = Instantiate(prefab);
        return instance;
    }
    protected T CreateGameObjectInstance<T>(T prefab, Transform parent) where T : MonoBehaviour
    {
        T instance = Instantiate(prefab, parent);
        return instance;
    }
}
