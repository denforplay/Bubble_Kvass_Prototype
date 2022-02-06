using System;
using Core;
using Core.Interfaces;
using Core.PopupSystem;
using Models.Collisions;
using Models.Systems;
using UnityEngine;
using Views;
using Views.Factories;
using Views.Popups;
using Views.Popups.MiniGamesPopups;
using Random = UnityEngine.Random;

namespace Models.MiniGames
{
    public class JumpMiniGame : IMiniGame
    {
        public event Action OnRestart;

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
            _jumpGamePopup.Character.OnBecomeInvisible += () =>
            {
                _platformSystem.StopAll();
                var popup = _popupSystem.SpawnPopup<LosePopup>();
                popup.OnRestart += Restart;
            };
            
            PlaceGameObjects();
        }

        private void PlaceGameObjects()
        {
            SpawnCharacter();
            var screenPosition = new Vector3(Screen.width/2, 0, 10);
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            _platformSpawner.Spawn(worldPosition);
            for (int i = 0; i < 100; i++)
            {
                var x = Random.Range(0, Screen.width);
                screenPosition = new Vector3(x, screenPosition.y + Screen.height / 5, 10);
                worldPosition = _camera.ScreenToWorldPoint(screenPosition);
                _platformSpawner.Spawn(worldPosition);
            }
        }

        public void Restart()
        {
            OnRestart?.Invoke();
            _popupSystem.DeletePopUp();
            PlaceGameObjects();
            _jumpGamePopup.Initialize(_collisionController, _character);
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
            view.OnBecomeInvisible += () => HidePlatform(view, platform);
        }

        private void HidePlatform(Transformable2DView platformView, Entity<Platform> platformEntity)
        {
            if (platformView.transform.position.y < _character.Position.y)
            {
                _platformSystem.OnEnd?.Invoke(platformEntity);
            }
        }
    }
}