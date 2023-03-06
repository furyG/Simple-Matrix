using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene
{
    private InteractorsBase interactorsBase;
    private RepositoryBase repositoriesBase;
    private SceneConfig sceneConfig;

    public Scene(SceneConfig config)
    {
        this.sceneConfig = config;
        this.interactorsBase = new InteractorsBase(config);
        this.repositoriesBase = new RepositoryBase(config);
    }

    public IEnumerator InitializeRoutine()
    {
        interactorsBase.CreateAllInteractors();
        repositoriesBase.CreateAllRepositories();

        yield return null;

        interactorsBase.SendOnCreateToAllInteractors();
        repositoriesBase.SendOnCreateToAllRepositories();

        yield return null;

        interactorsBase.SendOnInitializeToAllInteractors();
        repositoriesBase.SendOnInitializeToAllRepositories();

        yield return null;

        interactorsBase.SendOnStartToAllInteractors();
        repositoriesBase.SendOnStartToAllRepositories();
    }
    public T GetRepository<T>() where T : Repository
    {
         return this.repositoriesBase.GetRepository<T>();
    }
    public T GetInteractor<T>() where T : Interactor
    {
        return this.interactorsBase.GetInteractor<T>();
    }
}
