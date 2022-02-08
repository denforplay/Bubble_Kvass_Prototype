namespace Core.ObjectPool.Interfaces
{
    public interface IObjectPool
    {
        void ReturnToPool(object instance);
    }

    public interface IObjectPool<T> : IObjectPool where T : IPoolable
    {
        void ReturnToPool(T instance);
    }
}
