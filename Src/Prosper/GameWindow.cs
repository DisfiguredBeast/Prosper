using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Prosper.BaseGame;
using Prosper.BaseScene;
using Prosper.Core;
using Prosper.Core.Graphics.Data;
using Prosper.Library;

namespace Prosper
{
    public class GameWindow : OpenTK.GameWindow
    {
        private readonly GameConfig _gameConfig;
        private readonly List<GameObject> _cubes = new List<GameObject>();
        private int _index = 0;
        private Camera.Camera _camera;

        public GameWindow(GameConfig gameConfig) : base(800, 600, new GraphicsMode(32, 24, 0, 4), gameConfig.Title)
        {
            Guard.NotNull(gameConfig, nameof(gameConfig));
            _gameConfig = gameConfig;

            KeyDown += GameWindow_KeyDown;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(_gameConfig.ClearColor);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            GameInitializer.Initialize();
            GameSystemsRequirementValidator.Run();

            var cCube = Cube.Create(Color4.Red);
            cCube.Transform.Position = new Vector3(0f, 0f, 0f);
            _cubes.Add(cCube);

            cCube = Cube.Create(Color4.Blue);
            cCube.Transform.Position = new Vector3(-5f, 0f, 0f);
            _cubes.Add(cCube);

            cCube = Cube.Create(Color4.Green);
            cCube.Transform.Position = new Vector3(5f, 0f, 0f);
            _cubes.Add(cCube);

            cCube = Cube.Create(Color4.Gray);
            cCube.Transform.Position = new Vector3(0f, 5f, 0f);
            _cubes.Add(cCube);

            cCube = Cube.Create(Color4.DarkBlue);
            cCube.Transform.Position = new Vector3(0f, -5f, 0f);
            _cubes.Add(cCube);

            cCube = Cube.Create(Color4.Goldenrod);
            cCube.Transform.Position = new Vector3(0f, 0f, 5f);
            _cubes.Add(cCube);

            cCube = Cube.Create(Color4.MintCream);
            cCube.Transform.Position = new Vector3(0f, 0f, -5f);
            _cubes.Add(cCube);

            _camera = new Camera.Camera(this, new Vector3(0, 0, 10f), Vector3.Zero);
            GameContext.Camera = _camera;
            GameContext.GameWindow = this;

            var plane = Plane.Create(10, 0, 10);
            plane.Transform.Position = new Vector3(0f, -10f, 0f);
            plane.GetComponent<MeshComponent>().WireMode = true;

            OnUpdateFrame(new FrameEventArgs());
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!Focused)
                return;

            GameContext.Camera.Update(e.Time);
            GameContext.SystemRepository.UpdateSystems(e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GameContext.SystemRepository.DrawSystems(e.Time);

            GL.Flush();
            SwapBuffers();
        }

        private void GameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Exit();

            if (e.Key == Key.Space)
            {
                _index = _index == _cubes.Count - 1 ? -1 : _index + 1;
                var target = _index == -1 ? Vector3.Zero : _cubes[_index].Transform.Position;
                _camera.SetTarget(target);
            }
        }
    }
}
