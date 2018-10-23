using OpenTK.Input;
using Prosper.Core;

namespace Prosper.BaseScene
{
    public class OrbitController : IGameSystem
    {
        [Inject]
        public ComponentList<OrbitComponent> Orbiters { get; set; }

        private bool _left;
        private bool _right;
        private bool _up;
        private bool _down;

        public void Destroy() { }

        public void Draw(double dt) { }

        public void Init()
        {
            GameContext.GameWindow.KeyDown += (s, e) => OnKeyDown(e);
            GameContext.GameWindow.KeyUp += (s, e) => OnKeyUp(e);
        }

        public void Update(double dt)
        {
            foreach (var orbiter in Orbiters)
            {
                SetRotation(orbiter, dt);
                Move(orbiter);
            }
        }

        private void SetRotation(OrbitComponent orbiter, double dt)
        {
            var speed = (float)(360 * dt * orbiter.Speed);

            if (_left)
                orbiter.Orientation.X += speed;
            if (_right)
                orbiter.Orientation.X -= speed;

            if (_up)
                orbiter.Orientation.Y -= speed;
            if (_down)
                orbiter.Orientation.Y += speed;

            ClampY(orbiter);
            orbiter.Orientation.X = orbiter.Orientation.X % 360;
        }

        private void ClampY(OrbitComponent orbiter)
        {
            if (orbiter.Orientation.Y < -180f)
            {
                var y = -orbiter.Orientation.Y;
                orbiter.Orientation.Y = 360f - y % 180f;
            }
        }

        private void Move(OrbitComponent orbiter)
        {

        }

        #region Window Events

        private void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
                return;

            switch (e.Key)
            {
                case Key.A:
                    _left = true;
                    break;
                case Key.D:
                    _right = true;
                    break;
                case Key.W:
                    _up = true;
                    break;
                case Key.S:
                    _down = true;
                    break;
            }
        }

        private void OnKeyUp(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    _left = false;
                    break;
                case Key.D:
                    _right = false;
                    break;
                case Key.W:
                    _up = false;
                    break;
                case Key.S:
                    _down = false;
                    break;
            }
        }

        #endregion
    }
}
