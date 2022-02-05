using System;
using System.Collections.Generic;
using Core;
using Core.Interfaces;

namespace Models.Collisions
{
    public class CollisionRecords
    {
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided((Platform platform, Character character) =>
            {
                if((platform.Position - character.Position).y < 0)//CAN BE BUGS(JUST YET NOT OBSERVED)
                {
                    var characterVelocity = character.Velocity;
                    characterVelocity.y = 10f;
                    character.ChangeVelocity(characterVelocity);
                }
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}