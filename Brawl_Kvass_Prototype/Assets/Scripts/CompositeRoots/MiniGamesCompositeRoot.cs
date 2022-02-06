using Cinemachine;
using Configurations;
using Core.Abstracts;
using Core.Interfaces;
using Core.PopupSystem;
using Inputs;
using Models.Collisions;
using Models.MiniGames;
using UnityEngine;
using Views.Factories;
using Zenject;

namespace CompositeRoots
{
    public class MiniGamesCompositeRoot : CompositeRoot
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private Camera _camera;
        [SerializeField] private CollisionsCompositeRoot _collisionsRoot;
        [SerializeField] private PlatformFactory _platformFactory;
        private PopupSystem _popupSystem;
        private CollisionController _collisionController;
        private IMiniGame _currentGame;
        private IInputController _inputController;
        
        [Inject]
        public void Initialize(PopupSystem popupSystem)
        {
            _popupSystem = popupSystem;
        }

        private void Update()
        {
            _inputController?.Update();
        }

        public override void Compose()
        {
            _collisionController = _collisionsRoot.Controller;
            _platformFactory.Initialize(_collisionController);
        }

        public void StartJumpingGame()
        {
            _currentGame = new JumpMiniGame(_platformFactory, _popupSystem, _collisionController, _camera);
            _currentGame.OnEnable();
            _currentGame.OnStart();
            _inputController = new JumpGamePlayerControls(_currentGame.GetPopup().Character, _playerConfiguration);
            _inputController.OnEnable();
            _followCamera.BindTarget(_currentGame.GetPopup().Character.transform);
            _currentGame.OnRestart += _followCamera.Restart;
        }

        public void CancelGame()
        {
            _currentGame.OnEnd();
        }
    }
}