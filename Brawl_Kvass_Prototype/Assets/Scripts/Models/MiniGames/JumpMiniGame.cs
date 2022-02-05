using Core;
using Core.Interfaces;
using Core.PopupSystem;
using Models.Collisions;
using Models.Systems;
using UnityEngine;
using Views.Factories;
using Views.Popups;
using Views.Popups.MiniGamesPopups;

namespace Models.MiniGames
{
    public class JumpMiniGame : IMiniGame
    {
        private PlatformFactory _platformFactory;
        private PlatformSystem _platformSystem;
        private PlatformSpawner _platformSpawner;
        private CollisionController _collisionController;
        private PopupSystem _popupSystem;
        private Camera _camera;
        private Character _character;
        private JumpGamePopup _jumpGamePopup;
        public JumpGamePopup JumpGamePopup => _jumpGamePopup;

        public JumpMiniGame(PlatformFactory factory, PopupSystem popupSystem, CollisionController collisionController, Camera camera)
        {
            _platformFactory = factory;
            _popupSystem = popupSystem;
            _collisionController = collisionController;
            _camera = camera;
            _platformSystem = new PlatformSystem();
            _platformSpawner = new PlatformSpawner(_platformSystem, _camera);
        }

        public void OnStart()
        {
            SpawnCharacter();
            _jumpGamePopup = _popupSystem.SpawnPopup<JumpGamePopup>();
            _jumpGamePopup.Initialize(_collisionController, _character);
            var screenPosition = new Vector3(Screen.width / 2, 0, 10);
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            _platformSpawner.Spawn(worldPosition);
            _platformSpawner.Spawn();
            _platformSpawner.Spawn();
            _platformSpawner.Spawn();
        }

        public void OnEnd()
        {
            _popupSystem.DeletePopUp();
        }

        public void OnEnable()
        {
            _platformSystem.OnStart += SpawnPlatform;
            _platformSystem.OnEnd += _platformFactory.Destroy;
        }

        public void OnDisable()
        {
            _platformSystem.OnStart -= SpawnPlatform;
            _platformSystem.OnEnd -= _platformFactory.Destroy;
        }

        public MiniGamePopup GetPopup() => _jumpGamePopup;

        private void SpawnCharacter()
        {
            var positionInScreen = new Vector3(Screen.width / 2, Screen.height / 6);
            var startPosition = _camera.ScreenToWorldPoint(positionInScreen);
            _character = new Character(startPosition, Vector2.zero);
        }
        
        private void SpawnPlatform(Entity<Platform> platform)
        {
            var view = _platformFactory.Create(platform);
        }
    }
}