using Models.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models
{
    public class PlatformSpawner
    {
        private readonly PlatformSystem _system;
        private readonly Camera _camera;

        public PlatformSpawner(PlatformSystem system, Camera camera)
        {
            _system = system;
            _camera = camera;
        }

        public void Spawn(Vector2 position = default)
        {
            if (position == default)
            {
                _system.Work(CreateDefaultPlatform(GetRandomPositionInUpperPart()));
            }
            else
            {
                _system.Work(CreateDefaultPlatform(position));
            }
        }

        private Vector3 GetRandomPositionInUpperPart()
        {
            var randomX = Random.Range(0, Screen.width);
            var randomY = Random.Range(Screen.height / 3, Screen.height);
            return _camera.ScreenToWorldPoint(new Vector3(randomX, randomY, 10));
        }

        private Platform CreateDefaultPlatform(Vector2 position)
        {
            return new Platform(position, Vector2.zero);
        }
    }
}