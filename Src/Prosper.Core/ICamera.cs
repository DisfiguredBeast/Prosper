using OpenTK;

namespace Prosper.Core
{
    public interface ICamera
    {
        Matrix4 ViewMatrix { get; }
        Matrix4 ProjectionMatrix { get; }
        void Update(double dt);
    }
}
