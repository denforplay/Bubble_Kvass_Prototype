using Core.Abstracts;

namespace Core
{
    public sealed class Entity<T>
    {
        private readonly T _entity;
        private readonly Transformable2D _transformable;
        public T GetEntity => _entity;
        public Transformable2D Transformable => _transformable;
        public Entity(T entity, Transformable2D transformable)
        {
            _entity = entity;
            _transformable = transformable;
        }
    }
}