using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using Prosper.Graphics;
using Prosper.Graphics.Data;
using Prosper.Graphics.Shaders;

namespace Prosper.Core.Graphics.Systems
{
    public class MeshRenderer : IGameSystem
    {
        [Inject]
        public ComponentList<TransformComponent> Transform { get; set; }

        //[Inject]
        //public ComponentList<Mesh<T>> Transform { get; set; }

        //public readonly VertexArray VertexArray = new VertexArray();
        //public readonly VertexBuffer<T> Buffer = new VertexBuffer<T>();
        //public readonly IndexBuffer IndexBuffer = new IndexBuffer();
        //public readonly UniformMatrix4 ModelView = new UniformMatrix4("u_modelview");

        private readonly Shader _shader = Shader.Default;

        private float _time;

        public void Init()
        {
            //VertexArray.Bind();

            //Buffer.Bind();

            //var vertex = new T();
            //vertex.GetVertexAttributes().Do(v => v.EnableAttribute(_shader));

            //IndexBuffer.Bind();

            //VertexArray.Unbind();

            //ModelView.ResolveLocation(_shader);
        }

        public void Update(double dt)
        {
            _time += (float)dt;
        }

        public void Draw(double dt)
        {
            //var modelMatrix = Transform.GetModelMatrix();
            //var projectionMatrix = GameContext.ProjectionMatrix;
            //var modelViewMatrix = modelMatrix * projectionMatrix;
            //ModelView.Matrix = modelViewMatrix;

            //GL.UseProgram(_shader.Id);
            //ModelView.Set(_shader);

            //VertexArray.Bind();

            //GL.DrawElements(PrimitiveType.Triangles, IndexBuffer.Count, DrawElementsType.UnsignedInt, 0);

            //VertexArray.Unbind();
        }

        public void Destroy()
        {
        }
    }
}
