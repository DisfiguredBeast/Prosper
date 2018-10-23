using OpenTK;
using OpenTK.Graphics.OpenGL;
using Prosper.Graphics.Shaders;

namespace Prosper.Graphics.Data
{
    public struct Vertex : IVertexData
    {
        public Vector3 Position { get; set; }
        public Vector3 Color { get; set; }
        public Vector2 Texture { get; set; }

        public VertexAttribute[] GetVertexAttributes()
        {
            return new[]
            {
                new VertexAttribute("v_position", 3, VertexAttribPointerType.Float, 32, 0),
                new VertexAttribute("v_color", 3, VertexAttribPointerType.Float, 32, 12, true),
                new VertexAttribute("v_texCoords", 2, VertexAttribPointerType.Float, 32, 24),
            };
        }
    }
}
