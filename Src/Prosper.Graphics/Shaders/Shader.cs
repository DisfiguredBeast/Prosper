using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Prosper.Graphics.Shaders
{
    public class Shader : IDisposable
    {
        public static Shader Default => _default.Value;
        private static Lazy<Shader> _default = new Lazy<Shader>(LoadDefault);

        public readonly int Id;

        private readonly Dictionary<string, int> _attributes = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _uniforms = new Dictionary<string, int>();

        public Shader(int id)
        {
            Id = id;
        }

        public int GetAttributeLocation(string name)
        {
            return _attributes.GetOrAdd(name, () => GL.GetAttribLocation(Id, name));
        }

        public int GetUniformLocation(string name)
        {
            return _uniforms.GetOrAdd(name, () => GL.GetUniformLocation(Id, name));
        }

        private static Shader LoadDefault()
        {
            return new ShaderSource()
                .FromFile(ShaderType.VertexShader, @"_res\shaders\vertexShader.glsl")
                .FromFile(ShaderType.FragmentShader, @"_res\shaders\fragmentShader.glsl")
                .Load();
        }

        #region IDisposable Support
        private bool _disposed;

        private void DisposeCore()
        {
            if (_disposed)
                return;

            _disposed = true;
        }

        ~Shader()
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
