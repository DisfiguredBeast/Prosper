using System;
using OpenTK.Graphics.OpenGL;
using Prosper.Graphics.Data;
using Prosper.Graphics.Shaders;

namespace Prosper.Graphics
{
    public interface IVertexBuffer : IDisposable
    {
        void Bind();
        void Unbind();
        VertexAttribute[] GetVertexAttributes();
    }

    public sealed class VertexBuffer<T> : IVertexBuffer where T : struct, IVertexData
    {
        public readonly BufferTarget Target;

        private readonly int _id = GL.GenBuffer();
        private readonly int _size;

        public VertexBuffer(BufferTarget target = BufferTarget.ArrayBuffer)
        {
            Target = target;
            _size = System.Runtime.InteropServices.Marshal.SizeOf(new T());
        }

        public void Bind()
        {
            GL.BindBuffer(Target, _id);
        }

        public void Unbind()
        {
            GL.BindBuffer(Target, 0);
        }

        public void SetData(T[] data)
        {
            Bind();
            GL.BufferData(Target, (IntPtr)(data.Length * _size), data, BufferUsageHint.StaticDraw);
        }

        public VertexAttribute[] GetVertexAttributes()
        {
            return new T().GetVertexAttributes();
        }

        #region IDisposable Support
        private bool _disposed;

        private void DisposeCore()
        {
            if (_disposed)
                return;

            _disposed = true;
        }

        ~VertexBuffer()
        {
            // Do not change this code. Put cleanup code in Dispose() above.
            DisposeCore();
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose() above.
            DisposeCore();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
