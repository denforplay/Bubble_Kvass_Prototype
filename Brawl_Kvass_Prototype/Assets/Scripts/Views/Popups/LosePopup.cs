using System;
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
        
        public LosePopup SetCoinsText(int value)
        {
            _coinsText.text = value.ToString();
            return this;
        }
        
        public LosePopup SetGemsText(int value)
        {
            _gemsText.text = value.ToString();
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