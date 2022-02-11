using System.Linq;
using AppodealAds.Unity.Api;
using Configurations;
using Configurations.Info;
using Core.Abstracts;
using Core.PopupSystem;
using Models;
using UnityEngine;
using Views.Popups;
using Zenject;

namespace CompositeRoots
{
    public class GameCompositeRoot : CompositeRoot
    {
        [SerializeField] private MiniGamesConfiguration _miniGamesConfiguration;
        [SerializeField] private MiniGamesCompositeRoot _miniGamesRoot;
        private PopupSystem _popupSystem;
        private MainMenuPopup _mainMenuPopup;
        private MiniGameInfo _gameInfo;
        private PlayerData _playerData;

        [Inject]
        public void Initialize(PopupSystem popupSystem, PlayerData playerData)
        {
            _playerData = playerData;
            _popupSystem = popupSystem;
        }

        private void Start()
        {
            Appodeal.initialize("c800638c1b621bca247e7b942efb15dedc5e565ac02e1a01", Appodeal.INTERSTITIAL | Appodeal.BANNER_TOP, true);
            //Appodeal.show(Appodeal.BANNER_TOP);
            Debug.Log(_playerData.Coins);
        }

        public override void Compose()
        {
            _mainMenuPopup = _popupSystem.SpawnPopup<MainMenuPopup>();
            //_popupSystem.OnMainPopupVisible += () => Appodeal.show(Appodeal.INTERSTITIAL);
            _mainMenuPopup.OnChangeBackgroundClicked += CallChangeBackgroundPopup;
            _mainMenuPopup.OnFightersClicked += CallFightersPopup;
            _mainMenuPopup.OnChooseMiniGameButtonClicked += CallChooseMiniGamePopup;
            _mainMenuPopup.OnPlayButtonClicked += () => _miniGamesRoot.StartGame(_gameInfo);
            _playerData.OnBackgroundChanged += info => _mainMenuPopup.SetBackground(info.Sprite);
            _playerData.OnFighterChanged += info => _mainMenuPopup.SetFighter(info.FighterSprite);
            _playerData.OnMoneyChanged += _mainMenuPopup.SetCoinsText;
            _playerData.OnGemsChanged += _mainMenuPopup.SetGemsText;
            _playerData.Refresh();
            SetMiniGame(_miniGamesConfiguration.MiniGamesInfos.First());
        }

        private void OnDisable()
        {
            Debug.Log("Disable + data save");
            _playerData.SaveData();
        }

        private void OnDestroy()
        {
            Debug.Log("Destroyed game composite root");
            _playerData.SaveData();
        }

        private void CallChangeBackgroundPopup()
        {
            var backgroundPopup = _popupSystem.SpawnPopup<ChangeBackgroundPopup>();
            backgroundPopup.Initialize(_playerData.CurrentBackground.Sprite);
            backgroundPopup.OnBackgroundChanged += _playerData.SetBackground;
        }

        private void CallFightersPopup()
        {
            var fightersPopup = _popupSystem.SpawnPopup<FighterPopup>();
            fightersPopup.Initialize(_playerData.CurrentBackground.Sprite);
            fightersPopup.OnFighterClicked += CallFighterDescriptionPopup;
        }

        private void CallFighterDescriptionPopup(FighterInfo fighterInfo)
        {
            var fighterDescriptionPopup = _popupSystem.SpawnPopup<FighterDescriptionPopup>();
            fighterDescriptionPopup.Initialize(fighterInfo, _playerData.CurrentBackground.Sprite);
            fighterDescriptionPopup.OnChooseFighter += info =>
            {
                _playerData.SetFighter(info);
                _popupSystem.DeletePopUp();//Fighter description popup
                _popupSystem.DeletePopUp();//List of fighters popup
            };
        }

        private void CallChooseMiniGamePopup()
        {
            var miniGamesChoosePopup = _popupSystem.SpawnPopup<MinigamesChoosePopup>();
            miniGamesChoosePopup.Initialize(_playerData.CurrentBackground.Sprite);
            miniGamesChoosePopup.OnMiniGameClicked += SetMiniGame;
        }

        private void SetMiniGame(MiniGameInfo gameInfo)
        {
            _gameInfo = gameInfo;
            _mainMenuPopup.SetGameName(gameInfo.MiniGameName).SetGameMiniIcon(gameInfo.MiniGameMiniIcon).SetGameIcon(gameInfo.MiniGameIcon);
        }
    }
}