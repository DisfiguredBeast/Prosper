using Prosper.Core.Components;
using System;
using System.Collections.Generic;

namespace Prosper.Core
{
    public class GameObject
    {
        public readonly Guid Id;
        public readonly TransformComponent Transform;

        private readonly Dictionary<Type, IGameComponent> _components = new Dictionary<Type, IGameComponent>();

        public GameObject()
        {
            Id = Guid.NewGuid();
            Transform = new TransformComponent();
            AddComponent(Transform);
        }

        public void AddSystem<T>() where T : IGameSystem, new()
        {
            var system = GameContext.SystemRepository.GetSystem<T>();
            GameContext.ComponentRepository.Inject(this, system);
        }

        public void RemoveSystem<T>() where T : IGameSystem
        {
            var system = GameContext.SystemRepository.GetSystem<T>();
            GameContext.ComponentRepository.Remove(this, system);
        }

        public void AddComponent(IGameComponent component)
        {
            var type = component.GetType();
            _components.Add(type, component);
        }

        public T GetComponent<T>() where T : IGameComponent
        {
            var type = typeof(T);
            return (T)GetComponent(type);
        }

        public IGameComponent GetComponent(Type type)
        {
            if (_components.TryGetValue(type, out IGameComponent component))
                return component;

            var factory = GameContext.ComponentFactoryRepository.GetFactory(type);
            component = factory.Create(this);
            _components.Add(type, component);
            return component;
        }
    }
}
