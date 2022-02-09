using System;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class GamePausePopup : Popup
    {
        public event Action OnRestartClicked;
        public event Action OnMainMenuClicked;
        
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        private void Start()
        {
            Time.timeScale = 0;
            _continueButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                OnClosing();
            });
            
            _restartButton.onClick.AddListener(() =>
            {
                OnRestartClicked?.Invoke();
                Time.timeScale = 1;
                OnClosing();
            });
            
            _mainMenuButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                OnClosing();
                OnMainMenuClicked?.Invoke();
            });
        }

        public override void EnableInput()
        {
            _continueButton.interactable = true;
            _restartButton.interactable = true;
            _mainMenuButton.interactable = true;
        }

        public override void DisableInput()
        {
            _continueButton.interactable = false;
            _restartButton.interactable = false;
            _mainMenuButton.interactable = false;
        }
    }
}