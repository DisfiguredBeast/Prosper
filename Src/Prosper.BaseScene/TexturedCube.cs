using OpenTK;
using Prosper.Core;
using Prosper.Core.Graphics.Data;
using Prosper.Core.Graphics.Systems;
using Prosper.Graphics.Data;

namespace Prosper.BaseGame
{
    public static class TexturedCube
    {
        private const string Path = @"D:\Projects\Assets\Asteroid Strike.jpg";

        private static readonly Vertex[] _vertices =
        {
            new Vertex { Position = new Vector3(-0.5f, -0.5f,  -0.5f), Color = new Vector3(1f, 0f, 0f), Texture = new Vector2(0f, 0f) },
            new Vertex { Position = new Vector3(0.5f, -0.5f,  -0.5f), Color = new Vector3(0f, 0f, 1f), Texture = new Vector2(1f, 0f) },
            new Vertex { Position = new Vector3(0.5f, 0.5f,  -0.5f), Color = new Vector3(1f, 0f, 0f), Texture = new Vector2(1f, 1f)},
            new Vertex { Position = new Vector3(-0.5f, 0.5f,  -0.5f), Color = new Vector3(0f, 0f, 1f), Texture = new Vector2(0f, 1f) },

            new Vertex { Position = new Vector3(-0.5f, -0.5f,  0.5f), Color = new Vector3(0f, 0f, 1f), Texture = new Vector2(1f, 0f) },
            new Vertex { Position = new Vector3(0.5f, -0.5f,  0.5f), Color = new Vector3(1f, 0f, 0f), Texture = new Vector2(0f, 0f) },
            new Vertex { Position = new Vector3(0.5f, 0.5f,  0.5f), Color = new Vector3(0f, 0f, 1f), Texture = new Vector2(0f, 1f) },
            new Vertex { Position = new Vector3(-0.5f, 0.5f,  0.5f), Color = new Vector3(1f, 0f, 0f), Texture = new Vector2(1f, 1f) },
        };

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

        public static GameObject Create()
        {
            var instance = new GameObject();
            var meshComponent = instance.GetComponent<MeshComponent>();
            meshComponent.SetVertices(_vertices);
            meshComponent.SetIndices(_indices);

            var textureComponent = instance.GetComponent<TextureComponent>();
            textureComponent.HasTexture = true;
            textureComponent.TextureId = GameContext.ResourceManager.LoadImage(Path);

            instance.AddSystem<MeshRenderer>();
            return instance;
        }
    }
}
