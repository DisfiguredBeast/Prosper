using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prosper.Core
{
    public interface IComponentList
    {
        void Remove(int index);
    }

    public sealed class ComponentList<T> : IEnumerable<T>, IComponentList where T : IGameComponent
    {
        public int Count => _components.Count;

        public T this[int i] => _components[i];

        private List<T> _components = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_components).GetEnumerator();
        }

        public void Add(T component)
        {
            _components.Add(component);
        }

        public void Remove(int index)
        {
            _components = _components.Where((c, i) => i != index).ToList();
        }
    }
}
