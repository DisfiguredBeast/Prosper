using OpenTK;

namespace Prosper.Core
{
    public static class GameContext
    {
        public static SystemRepository SystemRepository { get; private set; } = new SystemRepository();
        public static ComponentFactoryRepository ComponentFactoryRepository { get; private set; } = new ComponentFactoryRepository();
        public static ComponentRepository ComponentRepository { get; internal set; } = new ComponentRepository();
        public static ResourceManager ResourceManager { get; internal set; } = new ResourceManager();

        public static ICamera Camera;
        public static GameWindow GameWindow;

        public static void Init()
        {
            ComponentFactoryRepository = new ComponentFactoryRepository();
            SystemRepository = new SystemRepository();
        }
    }
}
