using Prosper.Core.Graphics.Data;
using Prosper.Core.Graphics.Systems;

namespace Prosper.Core.Init
{
    internal sealed class CoreInitializer : InitializerBase
    {
        public override void RegisterComponentFactories()
        {
            RegisterComponentFactory<MeshComponent>(g => new MeshComponent());
            RegisterComponentFactory<TextureComponent>(g => new TextureComponent());
        }

        public override void RegisterSystems()
        {
            RegisterSystem(new MeshRenderer());
        }
    }
}
