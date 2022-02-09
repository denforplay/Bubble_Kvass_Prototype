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
        private readonly Dictionary<Transformable2DView, Action> _viewsActions = new Dictionary<Transformable2DView, Action>();
        private bool _isGameRun;
        private readonly Vector3 _barrierScreenSpawnPosition;
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
        }
        
        public void OnStart()
        {
            SpawnCharacter();
            _runGamePopup = _popupSystem.SpawnPopup<RunGamePopup>();
            _runGamePopup.Initialize(_collisionController, _character);
            _scoreSystem.OnScoreChanged += _runGamePopup.SetPoints;
            _scoreSystem.OnBestScoreChanged += _runGamePopup.SetBestPoints;
            _scoreSystem.Restart();
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
            PlaceGameObjects();
            _runGamePopup.Initialize(_collisionController, _character);
            _isGameRun = true;
        }

        private void OnLost()
        {
            _isGameRun = false;
            ClearViewActions();
            _scoreSystem.SaveBestScore();
            _scoreSystem.Restart();
            _barrierSystem.StopAll();
            var popup = _popupSystem.SpawnPopup<LosePopup>();
            popup.OnRestart += Restart;
            popup.OnMainMenuButtonClicked += OnEnd;
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
                var distance = Random.Range(Screen.width / 5, Screen.width / 3);
                copyStartPosition.x += distance;
                var worldPoint = _camera.ScreenToWorldPoint(copyStartPosition);
                _barrierSpawner.Spawn(worldPoint);
            }
        }

        private void SpawnBarrier(Entity<Barrier> barrier)
        {
            var view = _barrierFactory.Create(barrier);
            var velocity = Random.Range(_configuration.MinBarrierSpeed, _configuration.MaxBarrierSpeed);
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
                    _scoreSystem.AddScores(1);
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