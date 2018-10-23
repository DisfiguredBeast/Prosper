using OpenTK;
using Prosper.Core;
using Prosper.Core.Components;

namespace Prosper.BaseScene
{
    public class OrbitComponent : IGameComponent
    {
        public TransformComponent Target { get; set; }

        public float Distance { get; set; }
        public float Speed { get; set; }

        public Quaternion Orientation;// { get; set; }

        private readonly TransformComponent _transform;

        public OrbitComponent(GameObject gameObject)
        {
            _transform = gameObject.Transform;
        }
    }
}
