using System;
using System.Collections.Generic;
using System.Linq;

namespace Prosper.Core
{
    public class ComponentRepository
    {
        private readonly Dictionary<IGameSystem, Dictionary<Guid, int>> _componentIndices = new Dictionary<IGameSystem, Dictionary<Guid, int>>();
        private readonly ComponentInjector _componentInjector = new ComponentInjector();

        internal int Inject(GameObject gameObject, IGameSystem system)
        {
            var systemType = system.GetType();
            var systemIndices = _componentIndices.GetOrAdd(system, () => new Dictionary<Guid, int>());

            return systemIndices.GetOrAdd(gameObject.Id, () => _componentInjector.Inject(gameObject, system));
        }

        internal void Remove(GameObject gameObject, IGameSystem system)
        {
            if (!_componentIndices.TryGetValue(system, out Dictionary<Guid, int> systemIndices))
                return;

            if (!systemIndices.TryGetValue(gameObject.Id, out int index))
                return;

            _componentInjector.Remove(system, index);
            systemIndices.Remove(gameObject.Id);

            systemIndices = systemIndices.ToDictionary(kv => kv.Key, kv => kv.Value > index ? kv.Value - 1 : kv.Value);
            _componentIndices[system] = systemIndices;
        }
    }
}
