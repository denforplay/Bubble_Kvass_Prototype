using Core.Abstracts;
using Core.PopupSystem;
using UnityEngine;
using Views.Popups;
using Zenject;

namespace CompositeRoots
{
    public class GameCompositeRoot : CompositeRoot
    {
        private PopupSystem _popupSystem;
        private MainMenuPopup _mainMenuPopup;
        private Sprite _currentBackground;

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
        }

        private void CallChangeBackgroundPopup()
        {
            var backgroundPopup = _popupSystem.SpawnPopup<ChangeBackgroundPopup>();
            if (_currentBackground != null)
            {
                backgroundPopup.Initialize(_currentBackground);
            }
            
            backgroundPopup.OnBackgroundChanged += _mainMenuPopup.SetBackground;
            backgroundPopup.OnBackgroundChanged += SetCurrentBackground;
        }

        private void CallShopPopup()
        {
            var shopPopup = _popupSystem.SpawnPopup<ShopPopup>();
            if (_currentBackground != null)
            {
                shopPopup.Initialize(_currentBackground);
            }
        }

        private void CallFightersPopup()
        {
            var fightersPopup = _popupSystem.SpawnPopup<FighterPopup>();
            if (_currentBackground != null)
            {
                fightersPopup.Initialize(_currentBackground);
            } 
        }

        private void SetCurrentBackground(Sprite backgroundSprite)
        {
            _currentBackground = backgroundSprite;
        }
    }
}