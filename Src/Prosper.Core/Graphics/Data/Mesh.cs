using System.Collections.Generic;
using OpenTK;
using Prosper.Core;
using Prosper.Core.Graphics.Data;
using Prosper.Graphics.Shaders;

namespace Prosper.Graphics.Data
{
    public class Mesh<T> : IGameComponent where T : struct, IVertexData
    {
        public int VerticesCount { get; private set; }
        public int IndicesCount { get; private set; }

        public readonly VertexArray VertexArray = new VertexArray();
        public readonly VertexBuffer<T> Buffer = new VertexBuffer<T>();
        public readonly IndexBuffer IndexBuffer = new IndexBuffer();

        public Uniform[] Uniforms => _uniforms;

        private readonly Uniform[] _uniforms = new Uniform[] {
            new Uniform1("u_texture"),
            new UniformMatrix4("u_modelview")
        };

        public void SetAttributes(Shader shader)
        {
            VertexArray.Bind();

            Buffer.Bind();
            var vertex = new T();
            vertex.GetVertexAttributes().Do(v => v.EnableAttribute(shader));

            _uniforms.Do(u => u.ResolveLocation(shader));

            IndexBuffer.Bind();
            VertexArray.Unbind();
        }

        public void SetModelViewMatrix(Matrix4 matrix)
        {
            ((UniformMatrix4)_uniforms[0]).Matrix = matrix;
        }
    }
}
