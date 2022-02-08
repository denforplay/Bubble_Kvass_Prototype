namespace Core.ObjectPool.Interfaces
{
    public interface IPoolable
    {
        IObjectPool Origin { get; set; }
        void OnReturningInPool();
    }
}
