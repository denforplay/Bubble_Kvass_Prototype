using Core.Abstracts;
using UnityEngine;

namespace Models.Concretes
{
    public class Platform : Transformable2D
    {
        public Platform(Vector2 position, Vector2 velocity) : base(position, velocity)
        {
        }
    }
}
