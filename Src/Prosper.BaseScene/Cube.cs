using OpenTK;
using OpenTK.Graphics;
using Prosper.Core;
using Prosper.Core.Graphics.Data;
using Prosper.Core.Graphics.Systems;
using Prosper.Graphics.Data;

namespace Prosper.BaseGame
{
    public static class Cube
    {
        private static Vertex[] GetVertices(Vector3? color)
        {
            color = color ?? default;
            return new[] {
                new Vertex { Position = new Vector3(-0.5f, -0.5f,  -0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(0.5f, -0.5f,  -0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(0.5f, 0.5f,  -0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(-0.5f, 0.5f,  -0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(-0.5f, -0.5f,  0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(0.5f, -0.5f,  0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(0.5f, 0.5f,  0.5f), Color = color.Value },
                new Vertex { Position = new Vector3(-0.5f, 0.5f,  0.5f), Color = color.Value },
            };
        }

        private static readonly int[] _indices = {
            //left
            0, 2, 1,
            0, 3, 2,
            //back
            1, 2, 6,
            6, 5, 1,
            //right
            4, 5, 6,
            6, 7, 4,
            //top
            2, 3, 6,
            6, 3, 7,
            //front
            0, 7, 3,
            0, 4, 7,
            //bottom
            0, 1, 5,
            0, 5, 4
        };

        public static GameObject Create(Color4? color = default)
        {
            var c = color.HasValue ? new Vector3(color.Value.R, color.Value.G, color.Value.B) : default(Vector3?);
            return Create(c);
        }

        public static GameObject Create(Vector3? color = default)
        {
            var instance = new GameObject();
            var meshComponent = instance.GetComponent<MeshComponent>();

            var vertices = GetVertices(color);
            meshComponent.SetVertices(vertices);
            meshComponent.SetIndices(_indices);

            instance.AddSystem<MeshRenderer>();
            return instance;
        }
    }
}
