using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;
using Prosper.Core;

namespace Prosper.Graphics.Shaders
{
    public class ShaderSource
    {
        private readonly List<Tuple<ShaderType, string>> _sources = new List<Tuple<ShaderType, string>>();

        public ShaderSource FromFile(ShaderType shaderType, string path)
        {
            var src = new Tuple<ShaderType, string>(shaderType, File.ReadAllText(path));
            _sources.Add(src);
            return this;
        }

        public Shader Load()
        {
            var program = GL.CreateProgram();

            foreach (var source in _sources)
            {
                var shader = CreateShader(source.Item1, source.Item2);
                GL.AttachShader(program, shader);
            }

            GL.LinkProgram(program);
            Logger.Log(SeverityLevel.Debug, $"Shader {program} - {GL.GetProgramInfoLog(program)}");

            return new Shader(program);
        }

        private static int CreateShader(ShaderType shaderType, string source)
        {
            var shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);
            Logger.Log(SeverityLevel.Debug, $"{shaderType} - {GL.GetShaderInfoLog(shader)}");

            return shader;
        }
    }
}
