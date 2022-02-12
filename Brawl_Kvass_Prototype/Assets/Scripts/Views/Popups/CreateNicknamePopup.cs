using System;
using Core.PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class CreateNicknamePopup : Popup
    {
        public event Action<string> OnNameSetted;
        
        [SerializeField] private TMP_InputField _nicknameInputField;
        [SerializeField] private Button _setNicknameButton;

        private void Start()
        {
            _setNicknameButton.onClick.AddListener(ReadName);
        }

        private void ReadName()
        {
            if (!String.IsNullOrEmpty(_nicknameInputField.text))
            {
                OnNameSetted?.Invoke(_nicknameInputField.text);
                OnClosing();
            }
        }

        public override void EnableInput()
        {
            _setNicknameButton.interactable = true;
            _nicknameInputField.interactable = true;
        }

        public override void DisableInput()
        {
            _setNicknameButton.interactable = false;
            _nicknameInputField.interactable = false;
        }
    }
}