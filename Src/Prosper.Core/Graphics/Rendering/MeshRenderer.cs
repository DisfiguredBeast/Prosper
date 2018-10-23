using OpenTK.Graphics.OpenGL;
using Prosper.Core.Components;
using Prosper.Core.Graphics.Data;
using Prosper.Graphics.Shaders;

namespace Prosper.Core.Graphics.Systems
{
    public class MeshRenderer : IGameSystem
    {
        [Inject]
        public ComponentList<TransformComponent> Transforms { get; set; }

        [Inject]
        public ComponentList<MeshComponent> Meshes { get; set; }

        [Inject]
        public ComponentList<TextureComponent> Textures { get; set; }

        public readonly UniformMatrix4 ModelView = new UniformMatrix4("u_modelview");
        public readonly Uniform1 TextureUniform = new Uniform1("u_texture");

        private readonly Shader _shader = Shader.Default;

        private float _time;

        public void Init()
        {
        }

        public void Update(double dt)
        {
            _time += (float)dt;
        }

        public void Draw(double dt)
        {
            var cameraMatrix = GameContext.Camera.ViewMatrix * GameContext.Camera.ProjectionMatrix;

            for (var i = 0; i < Transforms.Count; i++)
            {
                var transform = Transforms[i];
                var mesh = Meshes[i];
                var texture = Textures[i];

                var modelMatrix = transform.GetModelMatrix();
                
                var modelViewMatrix = modelMatrix * cameraMatrix;
                ModelView.Matrix = modelViewMatrix;

                GL.UseProgram(mesh.Shader.Id);
                ModelView.Set(mesh.Shader);

                mesh.Bind();

                var srByTexture = GL.GetSubroutineIndex(mesh.Shader.Id, ShaderType.FragmentShader, "sr_byTexture");
                var srByColor = GL.GetSubroutineIndex(mesh.Shader.Id, ShaderType.FragmentShader, "sr_byColor");
                var uniform = GL.GetSubroutineUniformLocation(mesh.Shader.Id, ShaderType.FragmentShader, "u_subroutine");

                if (texture.HasTexture)
                {
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, texture.TextureId);
                    TextureUniform.ResolveLocation(mesh.Shader);
                    TextureUniform.Set(mesh.Shader);

                    GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref srByTexture);
                }
                else
                {
                    GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref srByColor);
                }

                if (mesh.WireMode)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

                GL.DrawElements(PrimitiveType.Triangles, mesh.IndexCount, DrawElementsType.UnsignedInt, 0);

                if (mesh.WireMode)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                mesh.Unbind();
            }
        }

        public void Destroy()
        {
        }
    }
}
