using OpenTK;

namespace Prosper.Core.Components
{
    public class TransformComponent : IGameComponent
    {
        public Vector3 Position { get; set; }
        public Quaternion Orientation { get; set; }
        public Vector3 Scale { get; set; } = Vector3.One;

        public GameObject GameObject { get; set; }

        public Matrix4 GetModelMatrix()
        {
            return Matrix4.CreateScale(Scale)
                * Matrix4.CreateRotationX(Orientation.X)
                * Matrix4.CreateRotationY(Orientation.Y)
                * Matrix4.CreateRotationZ(Orientation.Z)
                * Matrix4.CreateTranslation(Position);
        }

        public void Move(Vector3 delta)
        {
            Position += delta;
        }

        public void Move(float x, float y, float z)
        {
            Move(new Vector3(x, y, z));
        }
    }
}
