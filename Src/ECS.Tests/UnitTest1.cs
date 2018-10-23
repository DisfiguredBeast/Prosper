using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prosper.ECS.Components;

namespace Prosper.ECS.Tests
{
    public class TestComponent : IComponent { }
    public class TestComponentFactory : IComponentFactory<TestComponent>
    {
        public static TestComponentFactory Instance;

        public int Counter { get; private set; }

        public TestComponentFactory()
        {
            Instance = this;
        }

        public TestComponent Create()
        {
            Counter++;
            return new TestComponent();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var f = new ComponentManager();
            f.GatherFactories();
        }
    }
}
