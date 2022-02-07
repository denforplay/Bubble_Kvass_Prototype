using System;
using System.Collections.Generic;
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

namespace Models.MiniGames
{
    public class RunMiniGame : IMiniGame
    {
        private BarrierFactory _barrierFactory;
        private BarrierSystem _barrierSystem;
        private BarrierSpawner _barrierSpawner;
        private CollisionController _collisionController;
        private PopupSystem _popupSystem;
        private Camera _camera;
        private Character _character;
        private RunGamePopup _runGamePopup;
        private List<RunGameBarrierController> _controllers = new List<RunGameBarrierController>();
        private bool IsGameEnd = false;
        
        public event Action OnRestart;
        
        public RunMiniGame(BarrierFactory factory, PopupSystem popupSystem, CollisionController collisionController, Camera camera)
        {
            _barrierFactory = factory;
            _popupSystem = popupSystem;
            _collisionController = collisionController;
            _camera = camera;
            _barrierSystem = new BarrierSystem();
            _barrierSpawner = new BarrierSpawner(_barrierSystem, _camera);
        }
        
        public void OnStart()
        {
            SpawnCharacter();
            _runGamePopup = _popupSystem.SpawnPopup<RunGamePopup>();
            _runGamePopup.Initialize(_collisionController, _character);
            PlaceGameObjects();
        }

        public void Update()
        {
            _controllers.ForEach(c => c.Update(-2f));
        }

        public void Restart()
        {
        }

        public void OnEnd()
        {
        }

        public void OnEnable()
        {
            _barrierSystem.OnStart += SpawnBarrier;
            _barrierSystem.OnEnd += _barrierFactory.Destroy;
        }

        public void OnDisable()
        {
        }
        
        private void SpawnCharacter()
        {
            var positionInScreen = new Vector3(Screen.width / 5, Screen.height / 2);
            var startPosition = _camera.ScreenToWorldPoint(positionInScreen);
            _character = new Character(startPosition, Vector2.zero);
            _character.OnDestroyed += () =>
            {
                if (!IsGameEnd)
                {
                    IsGameEnd = true;
                    _barrierSystem.StopAll();
                    var popup = _popupSystem.SpawnPopup<LosePopup>();
                    popup.OnRestart += Restart;
                    popup.OnMainMenuButtonClicked += _popupSystem.DeletePopUp;
                }
            };
        }

        private void PlaceGameObjects()
        {
            SpawnCharacter();
            PlaceBarrier();
        }

        private void PlaceBarrier()
        {
            if (!IsGameEnd)
            {            
                var screenPosition = new Vector3(Screen.width - Screen.width/10, Screen.height/2, -10);
                var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
                _barrierSpawner.Spawn(worldPosition);
            }
        }

        private void SpawnBarrier(Entity<Barrier> barrier)
        {
            var view = _barrierFactory.Create(barrier);
            var controller = new RunGameBarrierController(view);
            _controllers.Add(controller);
            view.OnBecomeInvisible += () => HideBarrier(view, barrier);
            view.OnBecomeInvisible += () => _controllers.Remove(controller);
            view.OnBecomeInvisible += PlaceBarrier;
        }

        private void HideBarrier(Transformable2DView platformView, Entity<Barrier> barrierEntity)
        {
            if (platformView.transform.position.x < _character.Position.x)
            {
                _barrierSystem.OnEnd?.Invoke(barrierEntity);
            }
        }
        
        public MiniGamePopup GetPopup()
        {
            return _runGamePopup;
        }
    }
}