using System;
using System.Collections.Generic;

namespace Prosper.Core
{
    public class SystemRepository
    {
        private readonly Dictionary<Type, IGameSystem> _repository = new Dictionary<Type, IGameSystem>();

        public void RegisterSystem(IGameSystem system)
        {
            var type = system.GetType();
            if (_repository.ContainsKey(type))
                throw new ArgumentException($"There is already an instance of \"{type.FullName}\" registered.", nameof(system));

            _repository.Add(type, system);
        }

        public T GetSystem<T>() where T : IGameSystem
        {
            var type = typeof(T);
            return (T)_repository.Get(type);
        }

        public void UpdateSystems(double dt)
        {
            _repository.Values.Do(v => v.Update(dt));
        }

        public void DrawSystems(double dt)
        {
            _repository.Values.Do(v => v.Draw(dt));
        }
    }
}
