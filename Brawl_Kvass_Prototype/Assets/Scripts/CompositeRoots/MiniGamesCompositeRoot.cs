using System;
using Configurations;
using Configurations.Info;
using Core.Abstracts;
using Core.Enums;
using Core.Interfaces;
using Core.PopupSystem;
using Data;
using Inputs;
using Models.MiniGames;
using UnityEngine;
using Views.Factories;
using Views.Popups;
using Zenject;

namespace CompositeRoots
{
    public class MiniGamesCompositeRoot : CompositeRoot
    {
        [SerializeField] private JumpGameConfiguration _jumpGameConfiguration;
        [SerializeField] private RunGameConfiguration _runGameConfiguration;
        [SerializeField] private InputsConfiguration _inputsConfiguration;
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private Camera _camera;
        [SerializeField] private PlatformFactory _platformFactory;
        [SerializeField] private BarrierFactory _barrierFactory;
        private PopupSystem _popupSystem;
        private IMiniGame _currentGame;
        private IInputController _inputController;
        private PlayerDataProvider _playerDataProvider;
        
        [Inject]
        public void Initialize(PopupSystem popupSystem, PlayerDataProvider playerDataProvider)
        {
            _playerDataProvider = playerDataProvider;
            _popupSystem = popupSystem;
        }

        private void Update()
        {
            _inputController?.Update();
            _currentGame?.Update();
        }

        public override void Compose()
        {
        }

        public void StartGame(MiniGameInfo gameInfo)
        {
            switch (gameInfo.GameType)
            {
                case MiniGameType.JumpGame:
                {
                    StartJumpingGame();
                    break;
                }
                case MiniGameType.RunGame:
                {
                    StartRunningGame();
                    break;
                }
                default:
                    throw new NotImplementedException();
            }

            _currentGame.OnMoneyReceived += system =>
            {
                _playerDataProvider.PlayerData.ChangeMoney(system.CurrentCoins);
                _playerDataProvider.PlayerData.ChangeGems(system.CurrentGems);
            };
            
            _currentGame.GetPopup().OnPauseClicked += () =>
            {
                var gamePausePopup = _popupSystem.SpawnPopup<GamePausePopup>(1);
                gamePausePopup.OnRestartClicked += _currentGame.Restart;
                gamePausePopup.OnMainMenuClicked += _currentGame.OnEnd;
            };
            _currentGame.GetPopup().SetCharacterSprite(_playerDataProvider.FightersRepository.GetCurrent().FighterSprite);
        }

        private void StartJumpingGame()
        {
            _currentGame = new JumpMiniGame(_platformFactory, _popupSystem, _camera, _jumpGameConfiguration);
            _currentGame.OnEnable();
            _currentGame.OnStart();
            _inputController = new JumpGamePlayerControls(_currentGame.GetPopup().Character, _playerConfiguration, _inputsConfiguration);
            _inputController.OnEnable();
            _followCamera.BindTarget(_currentGame.GetPopup().Character.transform);
            _currentGame.OnRestart += _followCamera.Restart;
        }

        private void StartRunningGame()
        {
            _currentGame = new RunMiniGame(_barrierFactory, _popupSystem, _camera, _runGameConfiguration);
            _currentGame.OnEnable();
            _currentGame.OnStart();
            _inputController = new RunGamePlayerControls(_currentGame.GetPopup().Character, _inputsConfiguration);
            _inputController.OnEnable();
            _currentGame.OnRestart += _followCamera.Restart;
        }

        public void CancelGame()
        {
            _currentGame.OnEnd();
        }
    }
}