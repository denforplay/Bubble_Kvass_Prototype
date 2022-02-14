using System.Linq;
using AppodealAds.Unity.Api;
using Configurations;
using Configurations.Info;
using Core.Abstracts;
using Core.Enums;
using Core.PopupSystem;
using Data;
using UnityEngine;
using Views.Popups;
using Zenject;

namespace CompositeRoots
{
    public class GameCompositeRoot : CompositeRoot
    {
        [SerializeField] private PlayerIconsConfiguration _playerIconsConfig;
        [SerializeField] private MiniGamesConfiguration _miniGamesConfiguration;
        [SerializeField] private MiniGamesCompositeRoot _miniGamesRoot;
        private PopupSystem _popupSystem;
        private MainMenuPopup _mainMenuPopup;
        private MiniGameInfo _gameInfo;
        private PlayerDataProvider _playerDataProvider;

        [Inject]
        public void Initialize(PopupSystem popupSystem, PlayerDataProvider playerDataProvider)
        {
            _playerDataProvider = playerDataProvider;
            _popupSystem = popupSystem;
        }

        private void Start()
        {
            Appodeal.initialize("c800638c1b621bca247e7b942efb15dedc5e565ac02e1a01", Appodeal.INTERSTITIAL | Appodeal.BANNER_TOP, true);
            Appodeal.show(Appodeal.BANNER_TOP);
        }

        public override void Compose()
        {

            _mainMenuPopup = _popupSystem.SpawnPopup<MainMenuPopup>();
            _popupSystem.OnMainPopupVisible += () => Appodeal.show(Appodeal.INTERSTITIAL);
            _mainMenuPopup.OnChangeBackgroundClicked += CallChangeBackgroundPopup;
            _mainMenuPopup.OnFightersClicked += CallFightersPopup;
            _mainMenuPopup.OnChooseMiniGameButtonClicked += CallChooseMiniGamePopup;
            _mainMenuPopup.OnPlayButtonClicked += () => _miniGamesRoot.StartGame(_gameInfo);
            _mainMenuPopup.OnPlayerInfoClicked += CallPlayerInfoPopup;
            _playerDataProvider.BackgroundsRepository.OnCurrentEntityChanged += info => _mainMenuPopup.SetBackground(info.Sprite);
            _playerDataProvider.FightersRepository.OnCurrentEntityChanged += info => _mainMenuPopup.SetFighter(info.FighterSprite);
            _playerDataProvider.MoneyRepository.OnValueChanged += _mainMenuPopup.SetMoneyText;
            _playerDataProvider.PlayerData.OnNameChanged += _mainMenuPopup.SetPlayerName;
            _playerDataProvider.PlayerIconsRepository.OnCurrentEntityChanged += info => _mainMenuPopup.SetPlayerIcon(info.Icon);
            _playerDataProvider.Refresh();
            if (_playerDataProvider.PlayerData.PlayerNickname is null)
            {
                var createNicknamePopup = _popupSystem.SpawnPopup<CreateNicknamePopup>();
                createNicknamePopup.OnNameSetted += _playerDataProvider.PlayerData.SetName;
            }
            SetMiniGame(_miniGamesConfiguration.MiniGamesInfos.First());
        }

        private void OnDisable()
        {
            _playerDataProvider.Save();
        }

        private void CallChangeBackgroundPopup()
        {
            var backgroundPopup = _popupSystem.SpawnPopup<ChangeBackgroundPopup>();
            backgroundPopup.Initialize(_playerDataProvider.BackgroundsRepository.GetCurrent().Sprite, _playerDataProvider);
            backgroundPopup.OnBackgroundChanged += info => _playerDataProvider.BackgroundsRepository.SetCurrent(info.Id);
        }

        private void CallFightersPopup()
        {
            var fightersPopup = _popupSystem.SpawnPopup<FighterPopup>();
            fightersPopup.Initialize(_playerDataProvider.BackgroundsRepository.GetCurrent().Sprite);
            fightersPopup.OnFighterClicked += CallFighterDescriptionPopup;
        }

        private void CallFighterDescriptionPopup(FighterInfo fighterInfo)
        {
            var fighterDescriptionPopup = _popupSystem.SpawnPopup<FighterDescriptionPopup>();
            fighterDescriptionPopup.Initialize(fighterInfo, _playerDataProvider.BackgroundsRepository.GetCurrent().Sprite);
            fighterDescriptionPopup.OnChooseFighter += info =>
            {
                _playerDataProvider.FightersRepository.SetCurrent(info._id);
                _popupSystem.DeletePopUp();//Fighter description popup
                _popupSystem.DeletePopUp();//List of fighters popup
            };
        }

        private void CallChooseMiniGamePopup()
        {
            var miniGamesChoosePopup = _popupSystem.SpawnPopup<MinigamesChoosePopup>();
            miniGamesChoosePopup.Initialize(_playerDataProvider.BackgroundsRepository.GetCurrent().Sprite);
            miniGamesChoosePopup.OnMiniGameClicked += SetMiniGame;
        }

        private void CallPlayerInfoPopup()
        {
            var playerInfoPopup = _popupSystem.SpawnPopup<PlayerInfoPopup>();
            playerInfoPopup.SetCoinsForAllTime(_playerDataProvider.PlayerData.BestMoney.Money[MoneyType.Coin]).SetName(_playerDataProvider.PlayerData.PlayerNickname)
                .SetGemsForAllTime(_playerDataProvider.PlayerData.BestMoney.Money[MoneyType.Gem]).SetBackground(_playerDataProvider.BackgroundsRepository.GetCurrent().Sprite)
                .SetPlayerIcon(_playerDataProvider.PlayerIconsRepository.GetCurrent().Icon);
            playerInfoPopup.OnPlayerIconChangedClick += () => CallChangeIconPopup(playerInfoPopup);
        }

        private void CallChangeIconPopup(PlayerInfoPopup playerInfoPopup)
        {
            var changePlayerIconPopup = _popupSystem.SpawnPopup<ChangePlayerIconPopup>();
            changePlayerIconPopup.Initialize(_playerDataProvider.BackgroundsRepository.GetCurrent().Sprite, _playerIconsConfig);
            changePlayerIconPopup.OnPlayerIconChanged += info => _playerDataProvider.PlayerIconsRepository.SetCurrent(info.Id);
            changePlayerIconPopup.OnPlayerIconChanged += info => playerInfoPopup.SetPlayerIcon(info.Icon);
        }

        private void SetMiniGame(MiniGameInfo gameInfo)
        {
            _gameInfo = gameInfo;
            _mainMenuPopup.SetGameName(gameInfo.MiniGameName).SetGameMiniIcon(gameInfo.MiniGameMiniIcon).SetGameIcon(gameInfo.MiniGameIcon);
        }
    }
}