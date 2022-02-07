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
        public event Action OnChooseMinigameButtonClicked;
        public event Action OnPlayButtonClicked;

        [SerializeField] private Button _changeBackgroundButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _fighterButton;
        [SerializeField] private Button _chooseMinigameButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _background;
        [SerializeField] private Image _fighter;
        
        private void Start()
        {
            _changeBackgroundButton.onClick.AddListener(() => OnChangeBackgroundClicked?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopClicked?.Invoke());
            _fighterButton.onClick.AddListener(() => OnFightersClicked?.Invoke());
            _chooseMinigameButton.onClick.AddListener(() => OnChooseMinigameButtonClicked?.Invoke());
            _playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
        }

        public void SetBackground(Sprite sprite)
        {
            _background.sprite = sprite;
        }

        public void SetFighter(Sprite sprite)
        {
            _fighter.sprite = sprite;
        }

        public override void EnableInput()
        {
            _changeBackgroundButton.interactable = true;
            _fighterButton.interactable = true;
            _shopButton.interactable = true;
            _chooseMinigameButton.interactable = true;
            _playButton.interactable = true;
        }

        public override void DisableInput()
        {
            _changeBackgroundButton.interactable = false;
            _fighterButton.interactable = false;
            _shopButton.interactable = false;
            _chooseMinigameButton.interactable = false;
            _playButton.interactable = false;
        }
    }
}