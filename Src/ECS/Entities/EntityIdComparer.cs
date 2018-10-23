using System.Collections.Generic;

namespace Prosper.ECS.Entities
{
    internal class EntityIdComparer : IEqualityComparer<Entity>
    {
        public bool Equals(Entity x, Entity y)
        {
            return x == null && y == null || x != null && y != null && x.Id == y.Id;
        }

        public int GetHashCode(Entity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
