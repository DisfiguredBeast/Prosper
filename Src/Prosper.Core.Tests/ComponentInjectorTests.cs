using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prosper.Core.Tests
{
    [TestClass]
    public class ComponentInjectorTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            GameContext.Init();
        }

        [TestMethod]
        public void Inject()
        {
            var gameObject = new GameObject();
            var component = new TestComponent();
            gameObject.AddComponent(component);

            var system = new TestSystem();

            var injector = new ComponentInjector();
            var index = injector.Inject(gameObject, system);

            Assert.AreEqual(0, index);
            Assert.IsNotNull(system.Components);
            Assert.AreEqual(1, system.Components.Count);
            Assert.AreSame(component, system.Components[0]);
        }

        [TestMethod]
        public void Inject_NewComponent()
        {
            GameContext.ComponentFactoryRepository.RegisterFactory<TestComponent>(new TestComponentFactory());

            var gameObject = new GameObject();
            var system = new TestSystem();

            var injector = new ComponentInjector();
            var index = injector.Inject(gameObject, system);

            Assert.AreEqual(0, index);
            Assert.IsNotNull(system.Components);
            Assert.AreEqual(1, system.Components.Count);
        }

        [TestMethod]
        public void Inject_Thousand()
        {
            var gameObjects = Enumerable.Range(0, 1000).Select(i => new GameObject()).ToArray();
            var components = Enumerable.Range(0, 1000).Select(i => new TestComponent()).ToArray();

            for (var i = 0; i < gameObjects.Length; i++)
                gameObjects[i].AddComponent(components[i]);

            var system = new TestSystem();
            var injector = new ComponentInjector();

            for (var i = 0; i < gameObjects.Length; i++)
            {
                var index = injector.Inject(gameObjects[i], system);
                Assert.AreEqual(i, index);
                Assert.AreSame(components[i], system.Components[i]);
            }
        }

        [TestMethod]
        public void Inject_ThousandNewComponents()
        {
            GameContext.ComponentFactoryRepository.RegisterFactory<TestComponent>(new TestComponentFactory());

            var gameObjects = Enumerable.Range(0, 1000).Select(i => new GameObject()).ToArray();

            var system = new TestSystem();
            var injector = new ComponentInjector();

            for (var i = 0; i < gameObjects.Length; i++)
            {
                var index = injector.Inject(gameObjects[i], system);
                Assert.AreEqual(i, index);
            }
        }

        [TestMethod]
        public void AddSystemOnGameObject()
        {
            GameContext.ComponentFactoryRepository.RegisterFactory<TestComponent>(new TestComponentFactory());

            var system = new TestSystem();
            GameContext.SystemRepository.RegisterSystem(system);

            var gameObject = new GameObject();
            gameObject.AddSystem<TestSystem>();

            var component = gameObject.GetComponent<TestComponent>();
            Assert.IsNotNull(component);

            Assert.AreEqual(1, system.Components.Count);
            Assert.AreEqual(component, system.Components[0]);
        }

        [TestMethod]
        public void RemoveSystemOnGameObject()
        {
            GameContext.ComponentFactoryRepository.RegisterFactory<TestComponent>(new TestComponentFactory());

            var system = new TestSystem();
            GameContext.SystemRepository.RegisterSystem(system);

            var gameObject = new GameObject();
            gameObject.AddSystem<TestSystem>();
            Assert.AreEqual(1, system.Components.Count);

            gameObject.RemoveSystem<TestSystem>();
            Assert.AreEqual(0, system.Components.Count);
        }

        [TestMethod]
        public void RemoveSystemOnThousandGameObject()
        {
            var system = new TestSystem();
            GameContext.SystemRepository.RegisterSystem(system);
            GameContext.ComponentFactoryRepository.RegisterFactory<TestComponent>(new TestComponentFactory());

            var gameObjects = Enumerable.Range(0, 1000).Select(i => new GameObject()).ToArray();

            for (var i = 0; i < gameObjects.Length; i++)
                gameObjects[i].AddSystem<TestSystem>();

            Assert.AreEqual(gameObjects.Length, system.Components.Count);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (var i = 0; i < gameObjects.Length; i++)
                gameObjects[i].RemoveSystem<TestSystem>();

            stopWatch.Stop();
            System.Console.WriteLine(stopWatch.Elapsed);

            Assert.AreEqual(0, system.Components.Count);
        }
    }

    public class TestComponent : IGameComponent
    { }

    public class TestComponentFactory : IComponentFactory
    {
        public IGameComponent Create(GameObject gameObject)
        {
            return new TestComponent();
        }
    }

    public class TestSystem : IGameSystem
    {
        [Inject]
        public ComponentList<TestComponent> Components { get; set; }

        public void Destroy() { }
        public void Draw(double dt) { }
        public void Init() { }
        public void Update(double dt) { }
    }
}
