using System;
using Core.Enums;
using Core.PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class MainMenuPopup : Popup
    {
        public event Action OnShopClicked;
        public event Action OnChangeBackgroundClicked;
        public event Action OnFightersClicked;
        public event Action OnChooseMiniGameButtonClicked;
        public event Action OnPlayButtonClicked;
        public event Action OnPlayerInfoClicked;

        [SerializeField] private Button _changeBackgroundButton;
        [SerializeField] private Button _playerInfoButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _fighterButton;
        [SerializeField] private Button _chooseMinigameButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _background;
        [SerializeField] private Image _fighter;
        [SerializeField] private Image _miniGameMiniIcon;
        [SerializeField] private Image _miniGameIcon;
        [SerializeField] private Image _playerIcon;
        [SerializeField] private TextMeshProUGUI _gameName;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _gemsText;
        [SerializeField] private TextMeshProUGUI _playerName;
        
        private void Start()
        {
            _changeBackgroundButton.onClick.AddListener(() => OnChangeBackgroundClicked?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopClicked?.Invoke());
            _fighterButton.onClick.AddListener(() => OnFightersClicked?.Invoke());
            _chooseMinigameButton.onClick.AddListener(() => OnChooseMiniGameButtonClicked?.Invoke());
            _playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
            _playerInfoButton.onClick.AddListener(() => OnPlayerInfoClicked?.Invoke());
        }

        public void SetBackground(Sprite sprite)
        {
            _background.sprite = sprite;
        }

        public void SetFighter(Sprite sprite)
        {
            _fighter.sprite = sprite;
        }

        public MainMenuPopup SetGameName(string gameName)
        {
            _gameName.text = gameName;
            return this;
        }
        
        
        public MainMenuPopup SetGameMiniIcon(Sprite miniIcon)
        {
            _miniGameMiniIcon.sprite = miniIcon;
            return this;
        }
        
        public MainMenuPopup SetGameIcon(Sprite icon)
        {
            _miniGameIcon.sprite = icon;
            return this;
        }

        public MainMenuPopup SetPlayerIcon(Sprite icon)
        {
            _playerIcon.sprite = icon;
            return this;
        }

        public void SetPlayerName(string name)
        {
            _playerName.text = name;
        }

        public void SetMoneyText(MoneyType moneyType, int coins)
        {
            switch (moneyType)
            {
                case MoneyType.Coin:
                {
                    _coinsText.text = coins.ToString();
                    break;
                }
                case MoneyType.Gem:
                {
                    _gemsText.text = coins.ToString();
                    break;
                }
            }
        }

        public override void EnableInput()
        {
            _changeBackgroundButton.interactable = true;
            _fighterButton.interactable = true;
            _shopButton.interactable = true;
            _chooseMinigameButton.interactable = true;
            _playButton.interactable = true;
            _playerInfoButton.interactable = true;
        }

        public override void DisableInput()
        {
            _changeBackgroundButton.interactable = false;
            _fighterButton.interactable = false;
            _shopButton.interactable = false;
            _chooseMinigameButton.interactable = false;
            _playButton.interactable = false;
            _playerInfoButton.interactable = false;
        }
    }
}