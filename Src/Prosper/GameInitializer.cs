using System.Collections.Generic;
using Prosper.BaseScene;
using Prosper.Core.Init;

namespace Prosper
{
    internal static class GameInitializer
    {
        internal static void Initialize()
        {
            var initializers = GetInitializers();
            foreach (var initializer in initializers)
            {
                initializer.RegisterSystems();
                initializer.RegisterComponentFactories();
            }
        }

        private static IEnumerable<InitializerBase> GetInitializers()
        {
            yield return new CoreInitializer();
            yield return new BaseGameInitializer();
        }
    }
}
