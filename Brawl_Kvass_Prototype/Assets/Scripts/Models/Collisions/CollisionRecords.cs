using System;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using UnityEngine;

namespace Models.Collisions
{
    public class CollisionRecords
    {
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided((Platform platform, Character character) =>
            {
                Debug.Log("Collide");
                var characterVelocity = character.Velocity;
                characterVelocity.y = 10f;
                character.ChangeVelocity(characterVelocity);
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}