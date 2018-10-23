using Prosper.Core;
using System;

namespace Prosper.BaseScene
{
    public class OrbitSystem : IGameSystem
    {
        [Inject]
        public ComponentList<OrbitComponent> Orbiters { get; set; }

        public void Destroy() { }

        public void Draw(double dt) { }

        public void Init() { }

        public void Update(double dt)
        {
            throw new NotImplementedException();
        }
    }
}
