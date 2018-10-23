using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Prosper.Core;
using Prosper.Library;

namespace Prosper.Camera
{
    public class Camera : ICamera
    {
        public float FieldOfView = 60f;
        public float NearClip = 0.1f;
        public float FarClip = 100f;

        public Matrix4 ViewMatrix => GetViewMatrix();
        public Matrix4 ProjectionMatrix => GetProjectionMatrix();

        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _target;

        private Vector3 _cameraRight;
        private Vector3 _cameraUp;

        private float _distance = 20f;

        private readonly GameWindow _window;

        private float _maxRotation = MathHelper.DegreesToRadians(60f);
        private float _fullRotation = MathHelper.DegreesToRadians(360f);
        private float _rotationsPerSeconds = 0.5f;

        private float _rotationSpeed;
        private CameraControl[] _cameraControls;
        
        public Camera(GameWindow window, Vector3 position, Vector3 target)
        {
            Guard.NotNull(window, nameof(window));

            _window = window;
            _position = position;
            _rotationSpeed = _fullRotation * _rotationsPerSeconds;

            SetTarget(target);
            SetUpControls();

            _window.Resize += (s, e) => OnResize(e);
            _window.KeyDown += (s, e) => OnKeyDown(e);
            _window.KeyUp += (s, e) => OnKeyUp(e);
        }

        public void SetTarget(Vector3 target)
        {
            _target = target;
            _rotation = Vector3.Normalize(_position - target);

            _cameraRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, _rotation));
            _cameraUp = Vector3.Normalize(Vector3.Cross(_rotation, _cameraRight));
        }

        public void Update(double dt)
        {
            if (!_cameraControls.Any(c => c.AnyKeyDown))
                return;

            Rotate(dt);
            Move();

            _cameraRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, _rotation));
            _cameraUp = Vector3.Normalize(Vector3.Cross(_rotation, _cameraRight));
        }

        private void SetUpControls()
        {
            _cameraControls = new[]
            {
                new CameraControl(Key.W, Key.S, dt => _rotation.X += (float)dt * _rotationSpeed, dt => _rotation.X -= (float)dt * _rotationSpeed),
                new CameraControl(Key.A, Key.D, dt => _rotation.Y -= (float)dt * _rotationSpeed, dt => _rotation.Y += (float)dt * _rotationSpeed)
            };
        }

        private void Rotate(double dt)
        {
            foreach (var control in _cameraControls)
                control.OnUpdate(dt);

            _rotation.X = MathHelper.Clamp(_rotation.X, -_maxRotation, _maxRotation);
        }

        private void Move()
        {
            _position.X = (float)Math.Cos(_rotation.Y) * _distance + _target.X;
            _position.Y = (float)Math.Sin(_rotation.X) * _distance + _target.Y;
            _position.Z = (float)Math.Sin(_rotation.Y) * _distance - _target.Z;
        }

        private Matrix4 GetViewMatrix()
        {
            var position = new Vector3(_position.X, 0f, _position.Z);
            //return Matrix4.LookAt(_position, _target, _cameraUp);
            return Matrix4.LookAt(_position, _target, Vector3.UnitY);
        }

        private Matrix4 GetProjectionMatrix()
        {
            var fov = MathHelper.DegreesToRadians(FieldOfView);
            var aspect = (float)_window.Width / _window.Height;
            return Matrix4.CreatePerspectiveFieldOfView(fov, aspect, NearClip, FarClip);
        }

        private void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, _window.Width, _window.Height);
        }

        private void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
                return;

            foreach (var control in _cameraControls)
                control.OnKeyDown(e.Key);
        }

        private void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
                return;

            foreach (var control in _cameraControls)
                control.OnKeyUp(e.Key);
        }
    }
}

