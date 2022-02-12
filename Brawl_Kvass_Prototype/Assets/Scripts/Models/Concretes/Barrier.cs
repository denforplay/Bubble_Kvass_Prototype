using Core.Abstracts;
using UnityEngine;

namespace Models.Concretes
{
    public class Barrier : Transformable2D
    {
        public Barrier(Vector2 position, Vector2 velocity) : base(position, velocity)
        {
        }
    }
}