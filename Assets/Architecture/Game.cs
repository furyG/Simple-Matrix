using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Architecture
{
    public static class Game 
    {
        public static event Action OnGameInitializedEvent;

        private static Scene currentScene;
        public static SceneConfig sceneConfig;
        public static void Run()
        {
            sceneConfig = new SceneZeroConfig();
            currentScene = new Scene(sceneConfig);

            Coroutines.StartRoutine(currentScene.InitializeRoutine());
        }
        public static T GetInteractor<T>() where T : Interactor
        {
            return currentScene.GetInteractor<T>();
        }

        public static T GetRepository<T>() where T : Repository
        {
            return currentScene.GetRepository<T>();
        }
    }

}
