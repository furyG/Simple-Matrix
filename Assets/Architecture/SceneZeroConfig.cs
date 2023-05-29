using Architecture;
using System;
using System.Collections.Generic;

public class SceneZeroConfig : SceneConfig
{
    public const string SCENE_NAME = "SCENE_ZERO";

    public override string sceneName => sceneName;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        this.CreateInteractor<ConfigInteractor>(interactorsMap);
        this.CreateInteractor<PointsInteractor>(interactorsMap);
        this.CreateInteractor<LifesInteractor>(interactorsMap);
        this.CreateInteractor<TimerInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        this.CreateRepository<ConfigRepository>(repositoriesMap);
        this.CreateRepository<LifesRepository>(repositoriesMap);
        this.CreateRepository<PointsRepository>(repositoriesMap);
        this.CreateRepository<TimerRepository>(repositoriesMap);

        return repositoriesMap;
    }
}
