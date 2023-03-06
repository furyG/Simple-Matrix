using System;
using System.Collections.Generic;


namespace Architecture
{
    public class RepositoryBase
    {
        private Dictionary<Type, Repository> reposirtoryMap;
        private SceneConfig sceneConfig;

        public RepositoryBase(SceneConfig sceneConfig)
        {
            this.sceneConfig = sceneConfig;
        }
        public void CreateAllRepositories()
        {
            reposirtoryMap = sceneConfig.CreateAllRepositories();
        }

        public void SendOnCreateToAllRepositories()
        {
            var allRepositories = this.reposirtoryMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnCreate();
            }
        }
        public void SendOnInitializeToAllRepositories()
        {
            var allRepositories = this.reposirtoryMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.Initialize();
            }
        }
        public void SendOnStartToAllRepositories()
        {
            var allRepositories = this.reposirtoryMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnStart();
            }
        }
        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            return (T)this.reposirtoryMap[type];
        }
    }
}

