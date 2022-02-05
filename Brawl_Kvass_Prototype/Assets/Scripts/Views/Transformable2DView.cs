using Core.Abstracts;
using Core.ObjectPool.Interfaces;
using UnityEngine;

namespace Views
{
    public class Transformable2DView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        private Transformable2D _model;

        public Transformable2D Model => _model;

        public void Initialize(Transformable2D model)
        {
            _model = model;
            _model.OnVelocityChanged += SetVelocity;
        }

        private void FixedUpdate()
        {
            if (_rigidbody != null)
            {
                _model.Velocity = _rigidbody.velocity;
                _model.Position = _rigidbody.position;
            }
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void AddVelocity(Vector2 velocity)
        {
            _rigidbody.velocity += velocity;
        }

        public IObjectPool Origin { get; set; }
        public void OnReturningInPool()
        {
        }
    }
}