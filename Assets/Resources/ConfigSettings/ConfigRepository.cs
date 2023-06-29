using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigRepository : Repository
{
    public Dictionary<Type, ScriptableObject> settingsConfigMap { get; private set; }

    public override void OnCreate()
    {
        settingsConfigMap = new Dictionary<Type, ScriptableObject>();

        ScriptableObject[] _configs = Resources.LoadAll<ScriptableObject>("BalanceSettings");
        for(int i = 0; i < _configs.Length; i++)
        {
            settingsConfigMap.Add(_configs[i].GetType(), _configs[i]);
        }
    }
    public override void Initialize()
    {

    }

    public override void Save()
    {
        
    }
}
