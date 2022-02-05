using System;
using UnityEngine;

namespace Core.Abstracts
{
    public abstract class Transformable2D
    {
        public event Action<Vector2> OnVelocityChanged;
        
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        
        public Transformable2D(Vector2 position, Vector2 velocity)
        {
            Velocity = velocity;
            Position = position;
        }

        public void ChangeVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
            OnVelocityChanged?.Invoke(newVelocity);
        }
    }
}