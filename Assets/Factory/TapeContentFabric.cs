using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Tilemaps;
using UnityEngine;
using Tapes;

public class TapeContentFabric : GameObjectFabric
{
    public static TapeContentFabric instance;

    [SerializeField] private Tape tapePrefab;
    [SerializeField] private Number numberPrefab;
    [SerializeField] private Bonus bonusPrefab;
    [SerializeField] private FloatingPoints floatingPoints;
    [SerializeField] private Zero zeroPrefab;

    private void Awake()
    {
        instance = this;
    }

    public SpawnableObject Get(SpawnableType type)
    {
        return type switch
        {
            SpawnableType.Tape => Get(tapePrefab),
            SpawnableType.Number => Get(numberPrefab),
            SpawnableType.Bonus => Get(bonusPrefab),
            SpawnableType.Points=> Get(floatingPoints),
            SpawnableType.Zero => Get(zeroPrefab),
            _ => null,
        };
    }
    public SpawnableObject GetAndSetParent(SpawnableType type, Transform parent)
    {
        return type switch
        {
            SpawnableType.Tape => Get(tapePrefab, parent),
            SpawnableType.Number => Get(numberPrefab, parent),
            SpawnableType.Bonus => Get(bonusPrefab, parent),
            SpawnableType.Points => Get(floatingPoints, parent),
            SpawnableType.Zero => Get(zeroPrefab, parent),
            _ => null,
        };
    }

    private T Get<T>(T prefab) where T : SpawnableObject
    {
        T instance = CreateGameObjectInstance(prefab);
        return instance;
    }
    private T Get<T>(T prefab, Transform parent) where T : SpawnableObject
    {
        T instance = CreateGameObjectInstance(prefab, parent);
        return instance;
    }
}
