using System;
using Core.Enums;
using Core.PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class LosePopup : Popup
    {
        public event Action OnRestart;
        public event Action OnMainMenuButtonClicked;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _gemsText;
        
        private void Start()
        {
            _restartButton.onClick.AddListener(() =>
            {
                OnClosing();
                OnRestart?.Invoke();
            });
            _mainMenuButton.onClick.AddListener(() =>
            {
                OnClosing();
                OnMainMenuButtonClicked?.Invoke();
            });
        }

        public LosePopup SetScoreText(int value)
        {
            _scoreText.text = value.ToString();
            return this;
        }
        
        public LosePopup SetMoneyText(MoneyType moneyType, int value)
        {
            switch (moneyType)
            {
                case MoneyType.Coin:
                {
                    _coinsText.text = value.ToString();
                    break;
                }
                case MoneyType.Gem:
                {
                    _gemsText.text = value.ToString();
                    break;
                }
            }
            return this;
        }
        
        public override void EnableInput()
        {
            _restartButton.interactable = true;
            _mainMenuButton.interactable = true;
        }

        public override void DisableInput()
        {
            _restartButton.interactable = false;
            _mainMenuButton.interactable = false;
        }
    }
}