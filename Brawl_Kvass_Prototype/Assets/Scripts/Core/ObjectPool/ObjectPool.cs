using System;
using System.Collections.Concurrent;
using Core.ObjectPool.Interfaces;
using UnityEngine;

namespace Core.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T> where T: MonoBehaviour, IPoolable
    {
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();

        public T GetPrefabInstance(Func<T> instantiateMethod, Action<T> onTakingFromPool = null)
        {
            if (_container.Count > 0 && _container.TryTake(out var instance))
            {
                if (instance != null)
                {
                    instance.gameObject.SetActive(true);
                    onTakingFromPool?.Invoke(instance);
                }
            }
            else
            {
                instance = instantiateMethod.Invoke();
            }
            instance.Origin = this;
            return instance;
        }

        public void ReturnToPool(T instance)
        {
            instance.gameObject.gameObject.SetActive(false);
            instance.OnReturningInPool();
            _container.Add(instance);
        }

        public void ReturnToPool(object instance)
        {
            if (instance is T)
            {
                ReturnToPool(instance as T);
            }
        }
    }
}
