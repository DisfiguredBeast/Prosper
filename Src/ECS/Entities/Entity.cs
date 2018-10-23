using System;
using System.Collections.Generic;

namespace Prosper.ECS.Entities
{
    public class Entity
    {
        public Guid Id { get; }
        private readonly Dictionary<Type, IComponent> _components;

        internal Entity(Guid id)
        {
            Id = id;
        }
    }
}
