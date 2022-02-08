using Configurations.Info;
using Core.Abstracts;
using Core.PopupSystem;
using UnityEngine;
using Views.Popups;
using Zenject;

namespace CompositeRoots
{
    public class GameCompositeRoot : CompositeRoot
    {
        [SerializeField] private MiniGamesCompositeRoot _miniGamesRoot;
        private PopupSystem _popupSystem;
        private MainMenuPopup _mainMenuPopup;
        private Sprite _currentBackground;
        private Sprite _currentFighter;
        private MinigameInfo _gameInfo;

        [Inject]
        public void Initialize(PopupSystem popupSystem)
        {
            _popupSystem = popupSystem;
        }

        public override void Compose()
        {
            _mainMenuPopup = _popupSystem.SpawnPopup<MainMenuPopup>();
            _mainMenuPopup.OnChangeBackgroundClicked += CallChangeBackgroundPopup;
            _mainMenuPopup.OnShopClicked += CallShopPopup;
            _mainMenuPopup.OnFightersClicked += CallFightersPopup;
            _mainMenuPopup.OnChooseMinigameButtonClicked += CallChooseMinigamePopup;
            _mainMenuPopup.OnPlayButtonClicked += () => _miniGamesRoot.StartGame(_gameInfo);
        }

        private void CallChangeBackgroundPopup()
        {
            var backgroundPopup = _popupSystem.SpawnPopup<ChangeBackgroundPopup>();
            backgroundPopup.Initialize(_currentBackground);
            
            backgroundPopup.OnBackgroundChanged += _mainMenuPopup.SetBackground;
            backgroundPopup.OnBackgroundChanged += SetCurrentBackground;
        }

        private void CallShopPopup()
        {
            var shopPopup = _popupSystem.SpawnPopup<ShopPopup>();
            shopPopup.Initialize(_currentBackground);
        }

        private void CallFightersPopup()
        {
            var fightersPopup = _popupSystem.SpawnPopup<FighterPopup>();
            fightersPopup.Initialize(_currentBackground);
            fightersPopup.OnFighterClicked += CallFighterDescriptionPopup;
        }

        private void CallFighterDescriptionPopup(FighterInfo fighterInfo)
        {
            var fighterDescriptionPopup = _popupSystem.SpawnPopup<FighterDescriptionPopup>();
            fighterDescriptionPopup.Initialize(fighterInfo, _currentBackground);
            fighterDescriptionPopup.OnChooseFighter += info =>
            {
                SetCurrentFighter(info);
                _popupSystem.DeletePopUp();//Fighter description popup
                _popupSystem.DeletePopUp();//List of fighters popup
            };
        }

        private void CallChooseMinigamePopup()
        {
            var minigamesChoosePopup = _popupSystem.SpawnPopup<MinigamesChoosePopup>();
            minigamesChoosePopup.Initialize(_currentBackground);
            minigamesChoosePopup.OnMiniGameClicked += SetMiniGame;
        }

        private void SetMiniGame(MinigameInfo gameInfo)
        {
            _gameInfo = gameInfo;
        }

        private void SetCurrentBackground(Sprite backgroundSprite)
        {
            _currentBackground = backgroundSprite;
        }

        private void SetCurrentFighter(FighterInfo fighterInfo)
        {
            _currentFighter = fighterInfo.FighterSprite;
            _mainMenuPopup.SetFighter(_currentFighter);
        }
    }
}