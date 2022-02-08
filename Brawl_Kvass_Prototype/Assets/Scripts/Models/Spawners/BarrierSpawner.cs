using Models.Systems;
using UnityEngine;

namespace Models.Spawners
{
    public class BarrierSpawner
    {
        private readonly BarrierSystem _system;
        private readonly Camera _camera;

        public BarrierSpawner(BarrierSystem system, Camera camera)
        {
            _system = system;
            _camera = camera;
        }

        public void Spawn(Vector2 position)
        {
            _system.Work(CreateDefaultBarrier(position));
        }

        private Barrier CreateDefaultBarrier(Vector2 position)
        {
            return new Barrier(position, Vector2.zero);
        }
    }
}