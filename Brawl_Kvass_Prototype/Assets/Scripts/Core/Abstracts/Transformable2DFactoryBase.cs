using System.Collections.Generic;
using Models.Collisions;
using UnityEngine;
using Views;

namespace Core.Abstracts
{
    public abstract class Transformable2DFactoryBase<T> : MonoBehaviour
    {
        [SerializeField] private GameObject _viewContainer;
        private CollisionController _collisionController;
        private Dictionary<Entity<T>, Transformable2DView> _views = new Dictionary<Entity<T>, Transformable2DView>();
        private ObjectPool.ObjectPool<Transformable2DView> _entitiesPool = new ObjectPool.ObjectPool<Transformable2DView>();

        public void Initialize(CollisionController collisionController)
        {
            _collisionController = collisionController;
        }

        public Transformable2DView Create(Entity<T> entity)
        {
            Transformable2DView view = _entitiesPool.GetPrefabInstance(() => Instantiate(GetEntity(entity.GetEntity),
                entity.Transformable.Position,
                Quaternion.identity), (transformableView => transformableView.Initialize(entity.Transformable)));
            view.transform.SetParent(null);
            if (view.gameObject.TryGetComponent(out CollisionEvent collision))
            {
                collision.Initialize(_collisionController, entity.GetEntity);
            }
            view.Initialize(entity.Transformable);
            _views.Add(entity, view);
            view.transform.SetParent(_viewContainer.transform);
            return view;
        }

        public void Destroy(Entity<T> entity)
        {
            if (_views.TryGetValue(entity, out var findedView))
            {
                _views.Remove(entity);
                _entitiesPool.ReturnToPool(findedView);
            }
        }

        protected abstract Transformable2DView GetEntity(T entity);
    }
}