using Architecture;
using UnityEngine;

public class ConfigInteractor : Interactor
{
    private ConfigRepository _repository;

    public override void OnCreate()
    {
        _repository = Game.GetRepository<ConfigRepository>();
    }
    public T GetConfig<T>() where T : ScriptableObject
    {
        return (T)_repository.settingsConfigMap[typeof(T)];
    }
}
