using Core.Abstracts;
using Core.Interfaces;
using Core.PopupSystem;
using Inputs;
using Models.Collisions;
using Models.MiniGames;
using UnityEngine;
using Views.Factories;
using Views.Popups.MiniGamesPopups;
using Zenject;

namespace CompositeRoots
{
    public class MiniGamesCompositeRoot : CompositeRoot
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CollisionsCompositeRoot _collisionsRoot;
        [SerializeField] private PlatformFactory _platformFactory;
        private PopupSystem _popupSystem;
        private MiniGamePopup _popup;
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
            StartJumpingGame();
        }

        public void StartJumpingGame()
        {
            _currentGame = new JumpMiniGame(_platformFactory, _popupSystem, _collisionController, _camera);
            _currentGame.OnEnable();
            _currentGame.OnStart();
            _inputController = new JumpGamePlayerControls(_currentGame.GetPopup().Character);
            _inputController.OnEnable();
        }

        public void CancelGame()
        {
            _currentGame.OnEnd();
        }
    }
}