namespace Prosper.Core
{
    public interface IGameSystem
    {
        void Init();
        void Update(double dt);
        void Draw(double dt);
        void Destroy();
    }
}
