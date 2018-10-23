using System;
using OpenTK.Graphics.OpenGL;

namespace Prosper.Graphics
{
    public sealed class IndexBuffer
    {
        private const BufferTarget Target = BufferTarget.ElementArrayBuffer;

        public int Count { get; private set; }

        private readonly int _id = GL.GenBuffer();

        public void Bind()
        {
            GL.BindBuffer(Target, _id);
        }

        public void Unbind()
        {
            GL.BindBuffer(Target, 0);
        }

        public void SetData(int[] indices)
        {
            Bind();
            GL.BufferData(Target, (IntPtr)(indices.Length * sizeof(int)), indices, BufferUsageHint.StaticDraw);
            Count = indices.Length;
        }

        #region IDisposable Support
        private bool _disposed;

        private void DisposeCore()
        {
            if (_disposed)
                return;

            //GL.DeleteBuffer(_id);
            _disposed = true;
        }

        ~IndexBuffer()
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
