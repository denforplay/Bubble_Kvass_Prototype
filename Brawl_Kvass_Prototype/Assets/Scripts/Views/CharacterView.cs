using System;
using UnityEngine;

namespace Views
{
    public class CharacterView : Transformable2DView
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<PlatformView>(out _))
            {
                var characterVelocity = Model.Velocity;
                characterVelocity.y = 10f;
                Model.ChangeVelocity(characterVelocity);
            }
        }
    }
}