using System;

namespace Prosper.Core.Init
{
    public abstract class InitializerBase
    {
        public abstract void RegisterSystems();
        public abstract void RegisterComponentFactories();

        protected void RegisterSystem(IGameSystem system)
        {
            GameContext.SystemRepository.RegisterSystem(system);
        }

        protected void RegisterComponentFactory<T>(IComponentFactory factory) where T : IGameComponent
        {
            GameContext.ComponentFactoryRepository.RegisterFactory<T>(factory);
        }

        protected void RegisterComponentFactory<T>(Func<GameObject, IGameComponent> creator) where T : IGameComponent
        {
            var factory = new ComponentFactory(creator);
            RegisterComponentFactory<T>(factory);
        }
    }
}
