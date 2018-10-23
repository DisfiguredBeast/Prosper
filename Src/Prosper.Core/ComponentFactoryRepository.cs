using System;
using System.Collections.Generic;
using System.Reflection;

namespace Prosper.Core
{
    public class ComponentFactoryRepository
    {
        private readonly Dictionary<Type, MethodInfo> _genericGetFactories = new Dictionary<Type, MethodInfo>();
        private readonly Dictionary<Type, object> _factories = new Dictionary<Type, object>();

        public void RegisterFactory<T>(IComponentFactory factory) where T : IGameComponent
        {
            var type = typeof(T);
            if (_factories.ContainsKey(type))
                throw new ArgumentException($"There is already an instance of \"{type.FullName}\" registered.", nameof(factory));

            _factories.Add(type, factory);
        }

        internal IComponentFactory GetFactory<T>() where T : IGameComponent
        {
            var type = typeof(T);
            return (IComponentFactory)_factories.Get(type);
        }

        internal IComponentFactory GetFactory(Type type)
        {
            return (IComponentFactory)_factories.Get(type);
        }
    }
}
