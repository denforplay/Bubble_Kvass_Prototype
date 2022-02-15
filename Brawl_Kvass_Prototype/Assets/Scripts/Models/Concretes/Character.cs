using System;
using Core.Abstracts;
using UnityEngine;

namespace Models.Concretes
{
    public class Character : Transformable2D
    {
        public Action OnDestroyed;
        
        public Character(Vector2 position, Vector2 velocity) : base(position, velocity)
        {
        }
    }
}