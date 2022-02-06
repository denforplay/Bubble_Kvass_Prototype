using System;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class MainMenuPopup : Popup
    {
        public event Action OnShopClicked;
        public event Action OnChangeBackgroundClicked;
        public event Action OnFightersClicked;
        
        [SerializeField] private Button _changeBackgroundButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _fighterButton;
        [SerializeField] private Image _background;
        
        private void Start()
        {
            _changeBackgroundButton.onClick.AddListener(() => OnChangeBackgroundClicked?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopClicked?.Invoke());
            _fighterButton.onClick.AddListener(() => OnFightersClicked?.Invoke());
        }

        public void SetBackground(Sprite sprite)
        {
            _background.sprite = sprite;
        }

        public override void EnableInput()
        {
            _changeBackgroundButton.interactable = true;
            _fighterButton.interactable = true;
        }

        public override void DisableInput()
        {
            _changeBackgroundButton.interactable = false;
            _fighterButton.interactable = false;
        }
    }
}