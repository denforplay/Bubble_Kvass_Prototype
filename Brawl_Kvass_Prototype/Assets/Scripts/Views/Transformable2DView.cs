using System;
using Core.Abstracts;
using Core.ObjectPool.Interfaces;
using UnityEngine;

namespace Views
{
    public class Transformable2DView : MonoBehaviour, IPoolable
    {
        public event Action OnBecomeInvisible;
         
        [SerializeField] private Rigidbody2D _rigidbody;
        private Transformable2D _model;

        public Transformable2D Model => _model;

        public void Initialize(Transformable2D model)
        {
            _model = model;
            _model.OnVelocityChanged += SetVelocity;
            transform.position = model.Position;
            if (_rigidbody != null)
                _rigidbody.velocity = model.Velocity;
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

        public void SetVerticalVelocity(float velocity)
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, velocity);
            }
        }
        
        public void SetHorizontalVelocity(float velocity)
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector2(velocity, _rigidbody.velocity.y);
            }
        }

        public IObjectPool Origin { get; set; }
        public void OnReturningInPool()
        {
        }

        private void OnBecameInvisible()
        {
            OnBecomeInvisible?.Invoke();
        }
    }
}