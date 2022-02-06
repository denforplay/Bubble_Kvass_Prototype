using System;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class LosePopup : Popup
    {
        public event Action OnRestart;
        public event Action OnMainMenu;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(() => OnRestart?.Invoke());
            _mainMenuButton.onClick.AddListener(() => OnMainMenu?.Invoke());
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