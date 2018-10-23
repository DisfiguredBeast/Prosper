using OpenTK.Graphics.OpenGL;
using Prosper.Graphics.Shaders;

namespace Prosper.Graphics.Data
{
    public abstract class Uniform
    {
        protected readonly string Name;
        protected int Location { get; private set; }

        protected Uniform(string name)
        {
            Name = name;
        }

        public void ResolveLocation(Shader shader)
        {
            Location = GL.GetUniformLocation(shader.Id, Name);
        }

        public abstract void Set(Shader shader);
    }
}
