using System.Collections.Generic;
using Prosper.Graphics;
using Prosper.Graphics.Data;
using Prosper.Graphics.Shaders;

namespace Prosper.Core.Graphics.Data
{
    public class MeshComponent : IGameComponent
    {
        public int IndexCount => _indexBuffer.Count;

        public Shader Shader { get; private set; } = Shader.Default;

        public bool WireMode { get; set; }

        private readonly VertexArray _vertexArray = new VertexArray();
        private readonly IndexBuffer _indexBuffer = new IndexBuffer();
        private IVertexBuffer _vertexBuffer;

        public void Bind()
        {
            _vertexArray.Bind();
        }

        public void Unbind()
        {
            _vertexArray.Unbind();
        }

        public void SetVertices<T>(T[] data) where T : struct, IVertexData
        {
            _vertexArray.Bind();

            if (_vertexBuffer != null)
                _vertexBuffer.Dispose();

            var vertexBuffer = new VertexBuffer<T>();
            _vertexBuffer = vertexBuffer;
            vertexBuffer.SetData(data);

            EnableShaderAttribtes();

            _indexBuffer.Bind();

            _vertexArray.Unbind();
        }

        public void SetIndices(int[] indices)
        {
            _indexBuffer.SetData(indices);
        }

        public void SetShader(Shader shader)
        {
            if (shader == null)
                shader = Shader.Default;

            if (Shader == shader)
                return;

            Shader = shader;
            EnableShaderAttribtes();
        }

        private void EnableShaderAttribtes()
        {
            _vertexBuffer?.GetVertexAttributes().Do(v => v.EnableAttribute(Shader));
        }
    }
}
