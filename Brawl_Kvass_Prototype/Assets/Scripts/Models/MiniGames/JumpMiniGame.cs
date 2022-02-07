using System;
using System.Collections.Generic;
using Core;
using Core.Interfaces;
using Core.PopupSystem;
using Models.Collisions;
using Models.Spawners;
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

        private readonly PlatformFactory _platformFactory;
        private readonly PlatformSystem _platformSystem;
        private readonly PlatformSpawner _platformSpawner;
        private readonly CollisionController _collisionController;
        private readonly PopupSystem _popupSystem;
        private readonly Camera _camera;
        private Character _character;
        private JumpGamePopup _jumpGamePopup;
        private Vector2 _lastPlatformPosition;
        private readonly ScoreSystem _scoreSystem;
        private readonly Dictionary<Transformable2DView, Action> _viewsActions;
        public MiniGamePopup GetPopup() => _jumpGamePopup;
        public JumpMiniGame(PlatformFactory factory, PopupSystem popupSystem, CollisionController collisionController, Camera camera)
        {
            _viewsActions = new Dictionary<Transformable2DView, Action>();
            _platformFactory = factory;
            _popupSystem = popupSystem;
            _collisionController = collisionController;
            _camera = camera;
            _platformSystem = new PlatformSystem();
            _platformSpawner = new PlatformSpawner(_platformSystem, _camera);
            _scoreSystem = new ScoreSystem(nameof(JumpMiniGame));
        }

        public void OnStart()
        {
            SpawnCharacter();
            _jumpGamePopup = _popupSystem.SpawnPopup<JumpGamePopup>();
            _scoreSystem.OnScoreChanged += _jumpGamePopup.SetPoints;
            _scoreSystem.OnBestScoreChanged += _jumpGamePopup.SetBestPoints;
            _scoreSystem.Restart();
            _jumpGamePopup.Character.OnBecomeInvisible += OnLost;
            PlaceGameObjects();
        }

       
        public void Update()
        {
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
        
        private void OnLost()
        {
            foreach (var viewAction in _viewsActions)
            {
                viewAction.Key.OnBecomeInvisible -= viewAction.Value;
            }
            _viewsActions.Clear();
            _scoreSystem.SaveBestScore();
            _scoreSystem.Restart();
            _platformSystem.StopAll();
            var popup = _popupSystem.SpawnPopup<LosePopup>();
            popup.OnRestart += Restart;
            popup.OnMainMenuButtonClicked += OnEnd;
        }
        
        private void PlaceGameObjects()
        {
            SpawnCharacter();
            _lastPlatformPosition = new Vector3(Screen.width/2, 0, 10);
            var worldPosition = _camera.ScreenToWorldPoint(_lastPlatformPosition);
            _platformSpawner.Spawn(worldPosition);
            for (int i = 0; i < 5; i++)
            {
                SpawnOnePlatform();
            }
            
            _jumpGamePopup.Initialize(_collisionController, _character);
        }

        private void SpawnOnePlatform()
        {
            var x = Random.Range(0, Screen.width);
            _lastPlatformPosition = new Vector3(x, _lastPlatformPosition.y + Screen.height / 5, 10);
            var rememberedCameraPosition = _camera.transform.position;
            _camera.transform.position = new Vector3(rememberedCameraPosition.x, 0, rememberedCameraPosition.z);
            var worldPosition = _camera.ScreenToWorldPoint(_lastPlatformPosition);
            Debug.Log(worldPosition);
            _camera.transform.position = rememberedCameraPosition;
            _platformSpawner.Spawn(worldPosition);
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
            OnDisable();
            _platformSystem.StopAll();
            _scoreSystem.SaveBestScore();
        }

        private void SpawnCharacter()
        {
            var positionInScreen = new Vector3(Screen.width / 2, Screen.height / 6);
            var startPosition = _camera.ScreenToWorldPoint(positionInScreen);
            _character = new Character(startPosition, Vector2.zero);
        }
        
        private void SpawnPlatform(Entity<Platform> platform)
        {
            var view = _platformFactory.Create(platform);
            void Action()
            {
                SpawnOnePlatform();
                HidePlatform(view, platform);
            }

            _viewsActions.Add(view, Action);
            view.OnBecomeInvisible += Action;
        }

        private void HidePlatform(Transformable2DView platformView, Entity<Platform> platformEntity)
        {
            if (platformView.transform.position.y < _character.Position.y)
            {
                _scoreSystem.AddScores(1);
                _platformSystem.OnEnd?.Invoke(platformEntity);
                platformView.OnBecomeInvisible -= _viewsActions[platformView];
                _viewsActions.Remove(platformView);
                platformView.OnBecomeInvisible -= SpawnOnePlatform;
            }
        }
    }
}