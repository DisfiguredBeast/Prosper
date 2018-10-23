using System;
using System.Collections.Generic;

namespace Prosper.ECS.Entities
{
    public class EntityManager
    {
        private readonly HashSet<Entity> _entities = new HashSet<Entity>(new EntityIdComparer());

        public Entity CreateEntity()
        {
            var id = Guid.NewGuid();
            var entity = new Entity(id);
            _entities.Add(entity);
            return entity;
        }

        public void DeleteEntity(Entity entity)
        {
            _entities.Remove(entity);
        }
    }
}
