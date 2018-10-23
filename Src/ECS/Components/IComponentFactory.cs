namespace Prosper.ECS.Components
{
    public interface IComponentFactory<T> where T : IComponent
    {
        T Create();
    }
}
