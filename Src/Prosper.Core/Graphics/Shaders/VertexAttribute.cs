using OpenTK.Graphics.OpenGL;

namespace Prosper.Graphics.Shaders
{
    public sealed class VertexAttribute
    {
        public readonly string Name;
        public readonly int Size;
        public readonly VertexAttribPointerType Type;
        public readonly bool Normalize;
        public readonly int Stride;
        public readonly int Offset;

        public VertexAttribute(string name, int size, VertexAttribPointerType type, int stride, int offset, bool normalize = false)
        {
            Name = name;
            Size = size;
            Type = type;
            Stride = stride;
            Offset = offset;
            Normalize = normalize;
        }

        public void EnableAttribute(Shader shader)
        {
            var index = shader.GetAttributeLocation(Name);
            if (index == -1)
                return;

            GL.VertexAttribPointer(index, Size, Type, Normalize, Stride, Offset);
            GL.EnableVertexAttribArray(index);
        }
    }
}
