using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneZeroConfig : SceneConfig
{
    public const string SCENE_NAME = "SCENE_ZERO";

    public override string sceneName => sceneName;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        this.CreateInteractor<PointsInteractor>(interactorsMap);
        this.CreateInteractor<TimerInteractor>(interactorsMap);
        this.CreateInteractor<LevelInteractor>(interactorsMap);
        this.CreateInteractor<ButtonsInteractor>(interactorsMap);
        this.CreateInteractor<LifesInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        this.CreateRepository<PointsRepository>(repositoriesMap);
        this.CreateRepository<TimerRepository>(repositoriesMap);
        this.CreateRepository<LevelRepository>(repositoriesMap);
        this.CreateRepository<LifesRepository>(repositoriesMap) ;

        return repositoriesMap;
    }
}
