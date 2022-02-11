using System;
using System.Collections.Generic;
using Configurations;
using Core;
using Core.Interfaces;
using Core.PopupSystem;
using Inputs;
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
    public class RunMiniGame : IMiniGame
    {
        private readonly RunGameConfiguration _configuration;
        private readonly BarrierFactory _barrierFactory;
        private readonly BarrierSystem _barrierSystem;
        private readonly BarrierSpawner _barrierSpawner;
        private readonly CollisionController _collisionController;
        private readonly PopupSystem _popupSystem;
        private readonly Camera _camera;
        private Character _character;
        private RunGamePopup _runGamePopup;
        private readonly List<RunGameBarrierController> _controllers = new List<RunGameBarrierController>();
        private readonly ScoreSystem _scoreSystem;
        private readonly MoneySystem _moneySystem;
        private readonly Dictionary<Transformable2DView, Action> _viewsActions = new Dictionary<Transformable2DView, Action>();
        private bool _isGameRun;
        private readonly Vector3 _barrierScreenSpawnPosition;

        public event Action<MoneySystem> OnMoneyReceived;
        public event Action OnRestart;
        
        public MiniGamePopup GetPopup() => _runGamePopup;
        
        public RunMiniGame(BarrierFactory factory, PopupSystem popupSystem, CollisionController collisionController, Camera camera, RunGameConfiguration configuration)
        {
            _barrierFactory = factory;
            _popupSystem = popupSystem;
            _collisionController = collisionController;
            _camera = camera;
            _configuration = configuration;
            _barrierSystem = new BarrierSystem();
            _barrierSpawner = new BarrierSpawner(_barrierSystem, _camera);
            _scoreSystem = new ScoreSystem(nameof(RunMiniGame));
            _barrierScreenSpawnPosition = new Vector3(Screen.width + Screen.width / 10, Screen.height / 2, -10);
            _moneySystem = new MoneySystem();
        }
        
        public void OnStart()
        {
            SpawnCharacter();
            _runGamePopup = _popupSystem.SpawnPopup<RunGamePopup>();
            _runGamePopup.Initialize(_collisionController, _character);
            _scoreSystem.OnScoreChanged += _runGamePopup.SetPoints;
            _scoreSystem.OnBestScoreChanged += _runGamePopup.SetBestPoints;
            _scoreSystem.Restart();
            _moneySystem.OnGemsChanged += _runGamePopup.SetGemsText;
            _moneySystem.OnMoneyChanged += _runGamePopup.SetCoinsText;
            PlaceGameObjects();
        }

        public void Update()
        {
            _controllers.ForEach(c => c.Update());
        }
        
        public void Restart()
        {
            _isGameRun = false;
            OnRestart?.Invoke();
            ClearViewActions();
            _barrierSystem.StopAll();
            _scoreSystem.Restart();
            _moneySystem.Restart();
            PlaceGameObjects();
            _runGamePopup.Initialize(_collisionController, _character);
            _isGameRun = true;
        }

        private void OnLost()
        {
            _isGameRun = false;
            ClearViewActions();
            var popup = _popupSystem.SpawnPopup<LosePopup>(1);
            popup.SetScoreText(_scoreSystem.CurrentScore);
            popup.SetCoinsText(_moneySystem.CurrentCoins);
            popup.SetGemsText(_moneySystem.CurrentGems);
            OnMoneyReceived?.Invoke(_moneySystem);
            popup.OnRestart += Restart;
            popup.OnMainMenuButtonClicked += OnEnd;
            _scoreSystem.SaveBestScore();
            _scoreSystem.Restart();
            _barrierSystem.StopAll();
        }
        
        public void OnEnd()
        {
            ClearViewActions();
            _barrierSystem.StopAll();
            _scoreSystem.SaveBestScore();
            OnDisable();
            _popupSystem.DeletePopUp();
        }

        public void OnEnable()
        {
            _barrierSystem.OnStart += SpawnBarrier;
            _barrierSystem.OnEnd += _barrierFactory.Destroy;
        }

        public void OnDisable()
        {
            _barrierSystem.OnStart -= SpawnBarrier;
            _barrierSystem.OnEnd -= _barrierFactory.Destroy;
            _scoreSystem.OnScoreChanged -= _runGamePopup.SetPoints;
            _scoreSystem.OnBestScoreChanged -= _runGamePopup.SetBestPoints;
        }
        
        private void SpawnCharacter()
        {
            var positionInScreen = new Vector3(Screen.width / 5, Screen.height / 5);
            var startPosition = _camera.ScreenToWorldPoint(positionInScreen);
            _character = new Character(startPosition, Vector2.zero);
            _character.OnDestroyed += () =>
            {
                if (_isGameRun)
                {
                    OnLost();
                }
            };
        }

        private void PlaceGameObjects()
        {
            _isGameRun = true;
            SpawnCharacter();
            SpawnStartBarriers();
        }

        private void SpawnStartBarriers()
        {
            var copyStartPosition = _barrierScreenSpawnPosition;
            
            for (int i = 0; i < 5; i++)
            {
                var distance = Random.Range(Screen.width / 6, Screen.width/2);
                copyStartPosition.x += distance;
                var worldPoint = _camera.ScreenToWorldPoint(copyStartPosition);
                _barrierSpawner.Spawn(worldPoint);
            }
        }

        private void SpawnBarrier(Entity<Barrier> barrier)
        {
            var view = _barrierFactory.Create(barrier);
            var velocity = _configuration.MinBarrierSpeed;
            var controller = new RunGameBarrierController(view, velocity);
            _controllers.Add(controller);
            void Action()
            {
                HideBarrier(view, barrier);
                var copyStartPosition = _barrierScreenSpawnPosition;
                var distance = Random.Range(Screen.height / 5, Screen.height / 2);
                copyStartPosition.x += distance;
                _barrierSpawner.Spawn(_camera.ScreenToWorldPoint(copyStartPosition));
                _controllers.Remove(controller);
                if (_isGameRun)
                {
                    _scoreSystem.AddScores(1);
                    if (_scoreSystem.CurrentScore % _configuration.GemScoreNeeded == 0)
                        _moneySystem.ChangeGems(1);

                    if (_scoreSystem.CurrentScore % _configuration.CoinScoreNeeded == 0)
                        _moneySystem.ChangeCoins(1);
                }
            }

            _viewsActions.Add(view, Action);
            view.OnBecomeInvisible += Action;
        }

        private void HideBarrier(Transformable2DView barrierView, Entity<Barrier> barrierEntity)
        {
            if (barrierView.transform.position.x < _character.Position.x)
            {
                barrierView.OnBecomeInvisible -= _viewsActions[barrierView];
                _viewsActions.Remove(barrierView);
                _barrierSystem.OnEnd?.Invoke(barrierEntity);
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