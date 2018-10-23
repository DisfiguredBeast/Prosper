using System;
using Prosper.Library;

namespace Prosper.Core
{
    public class ComponentFactory : IComponentFactory 
    {
        private readonly Func<GameObject, IGameComponent> _creator;

        public ComponentFactory(Func<GameObject, IGameComponent> creator)
        {
            Guard.NotNull(creator, nameof(creator));
            _creator = creator;
        }

        public IGameComponent Create(GameObject gameObject)
        {
            return _creator(gameObject);
        }
    }
}
