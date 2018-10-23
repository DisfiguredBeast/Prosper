using OpenTK;
using Prosper.Core;
using Prosper.Core.Graphics.Data;
using Prosper.Core.Graphics.Systems;
using Prosper.Graphics.Data;

namespace Prosper.BaseScene
{
    public class Plane
    {
        private static readonly int[] _indices = {
            0, 2, 1,
            0, 3, 2,
        };

        public static GameObject Create(float x, float y, float z)
        {
            x /= 2f;
            y /= 2f;
            z /= 2f;

            var vertices = new []
            {
                new Vertex { Position = new Vector3(-x, -y, -z), Color = new Vector3(1f, 0f, 0f) },
                new Vertex { Position = new Vector3(x, y, -z), Color = new Vector3(0f, 0f, 1f) },
                new Vertex { Position = new Vector3(x, y, z), Color = new Vector3(0f, 1f, 0f) },
                new Vertex { Position = new Vector3(-x, -y, z), Color = new Vector3(1f, 0f, 0f) },
            };

            var instance = new GameObject();
            var meshComponent = instance.GetComponent<MeshComponent>();
            meshComponent.SetVertices(vertices);
            meshComponent.SetIndices(_indices);

            instance.AddSystem<MeshRenderer>();
            return instance;
        }
    }
}
