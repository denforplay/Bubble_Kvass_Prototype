using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Abstracts
{
    public abstract class SystemBase<T>
    {
        private List<Entity<T>> _entities = new List<Entity<T>>();

        public IEnumerable<Entity<T>> Entities => _entities;

        public event Action<Entity<T>> OnStart;
        public event Action<Entity<T>> OnEnd;

        public abstract void Update(float deltaTime);

        protected void Work(Entity<T> entity)
        {
            _entities.Add(entity);
            OnStart?.Invoke(entity);
        }

        protected void Stop(Entity<T> entity)
        {
            _entities.Remove(entity);
            OnEnd?.Invoke(entity);
        }

        public void StopAll(T model)
        {
            List<Entity<T>> candidats = _entities.Where(entity => entity.GetEntity.Equals(model)).ToList();
            candidats.ForEach(Stop);
        }
    }
}