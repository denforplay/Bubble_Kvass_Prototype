using System;
using Models;
using UnityEngine;

namespace Views
{
    public class BarrierView : Transformable2DView
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<CharacterView>(out var character))
            {
                (character.Model as Character)?.OnDestroyed?.Invoke();
            }
        }
    }
}