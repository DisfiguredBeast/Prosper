using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Prosper.Core
{
    public class ComponentInjector
    {
        private readonly Dictionary<IGameSystem, ComponentListGetter[]> _listGetters = new Dictionary<IGameSystem, ComponentListGetter[]>();
        private readonly Dictionary<Type, MethodInfo> _genericInjectors = new Dictionary<Type, MethodInfo>();

        private readonly MethodInfo _genericInjector;

        public ComponentInjector()
        {
            var parameterTypes = new[] { typeof(ComponentListGetter), typeof(GameObject), typeof(IGameSystem) };
            _genericInjector = GetType().GetMethod(nameof(Inject), BindingFlags.Instance | BindingFlags.NonPublic, null, parameterTypes, null);
        }

        public int Inject(GameObject gameObject, IGameSystem system)
        {
            var listGetters = GetComponentListGetters(system);

            int index = -1;
            foreach (var listGetter in listGetters)
            {
                if (!_genericInjectors.TryGetValue(listGetter.ComponentType, out MethodInfo injector))
                {
                    injector = _genericInjector.MakeGenericMethod(listGetter.ComponentType);
                    _genericInjectors.Add(listGetter.ComponentType, injector);
                }

                index = (int)injector.Invoke(this, new object[] { listGetter, gameObject, system });
            }

            return index;
        }

        public void Remove(IGameSystem system, int index)
        {
            var listGetters = GetComponentListGetters(system);
            foreach (var listGetter in listGetters)
            {
                var list = (IComponentList)listGetter.ListGetter.Invoke(system, null);
                list.Remove(index);
            }
        }

        private int Inject<T>(ComponentListGetter listGetter, GameObject gameObject, IGameSystem system) where T : IGameComponent
        {
            var list = (ComponentList<T>)listGetter.ListGetter.Invoke(system, null);
            var component = (T)gameObject.GetComponent(listGetter.ComponentType);
            list.Add(component);
            return list.Count - 1;
        }

        private IEnumerable<ComponentListGetter> GetComponentListGetters(IGameSystem system)
        {
            if (_listGetters.TryGetValue(system, out ComponentListGetter[] listGetters))
                return listGetters;

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            var type = system.GetType();
            var properties = type.GetProperties(flags)
                .Where(p => p.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            listGetters = properties.Select(p =>
            {
                var componentType = p.PropertyType.GetGenericArguments()[0]; // must be of type ComponentList<T>
                var getter = p.GetGetMethod(true);
                if (getter.Invoke(system, null) == null)
                {
                    var newList = Activator.CreateInstance(p.PropertyType);
                    var setter = p.GetSetMethod(true);
                    setter.Invoke(system, new[] { newList });
                }

                return new ComponentListGetter(componentType, getter);
            }).ToArray();

            _listGetters.Add(system, listGetters);
            return listGetters;
        }

        #region Nested

        private class ComponentListGetter
        {
            public readonly Type ComponentType;
            public readonly MethodInfo ListGetter;

            public ComponentListGetter(Type componentType, MethodInfo listGetter)
            {
                ComponentType = componentType;
                ListGetter = listGetter;
            }
        }

        #endregion
    }
}
