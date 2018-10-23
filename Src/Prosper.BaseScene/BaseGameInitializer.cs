using Prosper.Core.Init;

namespace Prosper.BaseScene
{
    public class BaseGameInitializer : InitializerBase
    {
        public override void RegisterComponentFactories()
        {
            RegisterComponentFactory<OrbitComponent>(g => new OrbitComponent(g));
        }

        public override void RegisterSystems()
        {
            //RegisterSystem(new OrbitSystem());
        }
    }
}
