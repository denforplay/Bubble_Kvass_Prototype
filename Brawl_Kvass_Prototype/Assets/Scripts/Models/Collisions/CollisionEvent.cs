using System;
using UnityEngine;

namespace Models.Collisions
{
    public class CollisionEvent : MonoBehaviour
    {
        private CollisionController _collisionController;
        private object _model;

        public void Initialize(CollisionController controller, object model)
        {
            _collisionController = controller;
            _model = model;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out CollisionEvent collisionEvent))
            {
                try
                {
                    _collisionController.TryCollide((_model, collisionEvent._model));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}