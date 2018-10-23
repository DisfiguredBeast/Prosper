using OpenTK;
using OpenTK.Graphics.OpenGL;
using Prosper.Graphics.Shaders;

namespace Prosper.Core.Graphics.Data
{
    public class UniformMatrix4 : Uniform
    {
        public Matrix4 Matrix;

        public UniformMatrix4(string name) : this(name, Matrix4.Identity) { }
        public UniformMatrix4(string name, Matrix4 matrix) : base(name)
        {
            Matrix = matrix;
        }

        public override void Set(Shader shader)
        {
            GL.UniformMatrix4(Location, false, ref Matrix);
        }
    }
}
