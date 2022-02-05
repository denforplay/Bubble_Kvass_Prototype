using Core.Abstracts;
using UnityEngine;

namespace Models
{
    public class Character : Transformable2D
    {
        public Character(Vector2 position, Vector2 velocity) : base(position, velocity)
        {
        }
    }
}