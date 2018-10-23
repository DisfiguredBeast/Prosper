using OpenTK.Graphics.OpenGL;
using Prosper.Graphics.Shaders;

namespace Prosper.Core.Graphics.Data
{
    public class Uniform1 : Uniform
    {
        public int Value;

        public Uniform1(string name) : this(name, default(int)) { }
        public Uniform1(string name, int value) : base(name)
        {
            Value = value;
        }

        public override void Set(Shader shader)
        {
            GL.Uniform1(Location, (uint)Value);
        }
    }
}
