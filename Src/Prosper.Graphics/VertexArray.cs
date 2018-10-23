using System;
using OpenTK.Graphics.OpenGL;

namespace Prosper.Graphics
{
    public sealed class VertexArray : IDisposable
    {
        private readonly int _id = GL.GenVertexArray();

        public void Bind()
        {
            GL.BindVertexArray(_id);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        #region IDisposable Support
        private bool _disposed; 

        private void DisposeCore()
        {
            if (_disposed)
                return;

            if (_id == 0)
                return;

            _disposed = true;
        }

        ~VertexArray()
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
