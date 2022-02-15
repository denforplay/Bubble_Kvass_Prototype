using System;
using Core.PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class PlayerInfoPopup : Popup
    {
        public event Action OnPlayerIconChangedClick;
        
        [SerializeField] private Button _changePlayerIconButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _coinsForAllTimeText;
        [SerializeField] private TextMeshProUGUI _gemsForAllTimeText;
        [SerializeField] private TextMeshProUGUI _playerName;
        private void Start()
        {
            _changePlayerIconButton.onClick.AddListener(() => OnPlayerIconChangedClick?.Invoke());
            _returnButton.onClick.AddListener(OnClosing);
        }

        public PlayerInfoPopup SetCoinsForAllTime(int coins)
        {
            _coinsForAllTimeText.text = coins.ToString();
            return this;
        }
        
        public PlayerInfoPopup SetGemsForAllTime(int coins)
        {
            _gemsForAllTimeText.text = coins.ToString();
            return this;
        }

        public PlayerInfoPopup SetBackground(Sprite backgroundSprite)
        {
            _background.sprite = backgroundSprite;
            return this;
        }

        public PlayerInfoPopup SetName(string name)
        {
            _playerName.text = name;
            return this;
        }

        public PlayerInfoPopup SetPlayerIcon(Sprite icon)
        {
            _changePlayerIconButton.image.sprite = icon;
            return this;
        }
        
        public override void EnableInput()
        {
            _changePlayerIconButton.interactable = true;
            _returnButton.interactable = true;
        }

        public override void DisableInput()
        {
            _changePlayerIconButton.interactable = false;
            _returnButton.interactable = false;
        }
    }
}