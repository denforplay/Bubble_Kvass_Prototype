using System;
using System.Collections.Generic;
using Configurations;
using Core;
using Core.Interfaces;
using Core.PopupSystem;
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
        public event Action<MoneySystem> OnMoneyReceived;
        public event Action OnRestart;

        private JumpGameConfiguration _configuration;
        private readonly PlatformFactory _platformFactory;
        private readonly PlatformSystem _platformSystem;
        private readonly PlatformSpawner _platformSpawner;
        private readonly PopupSystem _popupSystem;
        private readonly Camera _camera;
        private Character _character;
        private JumpGamePopup _jumpGamePopup;
        private Vector2 _lastPlatformPosition;
        private readonly ScoreSystem _scoreSystem;
        private readonly Dictionary<Transformable2DView, Action> _viewsActions;
        private bool _isGameRunning;
        private MoneySystem _moneySystem;
        
        public MiniGamePopup GetPopup() => _jumpGamePopup;
        public JumpMiniGame(PlatformFactory factory, PopupSystem popupSystem, Camera camera, JumpGameConfiguration configuration)
        {
            _viewsActions = new Dictionary<Transformable2DView, Action>();
            _configuration = configuration;
            _platformFactory = factory;
            _popupSystem = popupSystem;
            _camera = camera;
            _platformSystem = new PlatformSystem();
            _platformSpawner = new PlatformSpawner(_platformSystem, _camera);
            _scoreSystem = new ScoreSystem(nameof(JumpMiniGame));
            _moneySystem = new MoneySystem();
        }

        public void OnStart()
        {
            SpawnCharacter();
            _jumpGamePopup = _popupSystem.SpawnPopup<JumpGamePopup>();
            _scoreSystem.OnScoreChanged += _jumpGamePopup.SetPoints;
            _scoreSystem.OnBestScoreChanged += _jumpGamePopup.SetBestPoints;
            _moneySystem.OnGemsChanged += _jumpGamePopup.SetGemsText;
            _moneySystem.OnMoneyChanged += _jumpGamePopup.SetCoinsText;
            _scoreSystem.Restart();
            _jumpGamePopup.Character.OnBecomeInvisible += () =>
            {
                if (_isGameRunning)
                    OnLost();
            };
            PlaceGameObjects();
            _isGameRunning = true;
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
            _isGameRunning = false;
            foreach (var viewAction in _viewsActions)
            {
                viewAction.Key.OnBecomeInvisible -= viewAction.Value;
            }
            _viewsActions.Clear();
            var popup = _popupSystem.SpawnPopup<LosePopup>();
            popup.SetScoreText(_scoreSystem.CurrentScore).SetCoinsText(_moneySystem.CurrentCoins)
                .SetGemsText(_moneySystem.CurrentGems);
            OnMoneyReceived?.Invoke(_moneySystem);
            _scoreSystem.SaveBestScore();
            _scoreSystem.Restart();
            _moneySystem.Restart();
            _platformSystem.StopAll();
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
            
            _jumpGamePopup.Initialize(_character);
        }

        private void SpawnOnePlatform()
        {
            var x = Random.Range(0, Screen.width);
            _lastPlatformPosition = new Vector3(x, _lastPlatformPosition.y + Screen.height / 5, 10);
            var rememberedCameraPosition = _camera.transform.position;
            _camera.transform.position = new Vector3(rememberedCameraPosition.x, 0, rememberedCameraPosition.z);
            var worldPosition = _camera.ScreenToWorldPoint(_lastPlatformPosition);
            _camera.transform.position = rememberedCameraPosition;
            _platformSpawner.Spawn(worldPosition);
        }

        public void Restart()
        {
            _isGameRunning = false;
            OnRestart?.Invoke();
            ClearViewActions();
            _platformSystem.StopAll();
            _scoreSystem.Restart();
            PlaceGameObjects();
            _jumpGamePopup.Initialize(_character);
            _isGameRunning = true;
        }

        public void OnEnd()
        {
            _isGameRunning = false;
            ClearViewActions();
            _platformSystem.StopAll();
            _scoreSystem.SaveBestScore();
            OnDisable();
            _popupSystem.DeletePopUp();
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
                if (_scoreSystem.CurrentScore % _configuration.CoinScoreNeeded == 0)
                    _moneySystem.ChangeCoins(1);
                if (_scoreSystem.CurrentScore % _configuration.GemScoreNeeded == 0)
                    _moneySystem.ChangeGems(1);
                _platformSystem.OnEnd?.Invoke(platformEntity);
                platformView.OnBecomeInvisible -= _viewsActions[platformView];
                _viewsActions.Remove(platformView);
                platformView.OnBecomeInvisible -= SpawnOnePlatform;
            }
        }
        
        private void ClearViewActions()
        {
            foreach (var viewAction in _viewsActions)
            {
                viewAction.Key.OnBecomeInvisible -= viewAction.Value;
            }
            _viewsActions.Clear();
        }
    }
}