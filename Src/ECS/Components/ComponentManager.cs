using Prosper.Library;
using System.Linq;

namespace Prosper.ECS.Components
{
    public class ComponentManager
    {
        public void GatherFactories()
        {
            var factories = TypeHelper.GetAssignableTypes(typeof(IComponentFactory<>));
            foreach (var f in factories)
            {
                var @if = f.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IComponentFactory<>));
                // TODO: Get componentType
            }
        }
    }
}
