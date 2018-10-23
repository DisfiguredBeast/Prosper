namespace Prosper.Core
{
    public interface IComponentFactory
    {
        IGameComponent Create(GameObject gameObject);
    }
}
