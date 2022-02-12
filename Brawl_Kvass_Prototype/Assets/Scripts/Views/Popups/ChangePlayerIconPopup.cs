using System;
using Configurations;
using Configurations.Info;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class ChangePlayerIconPopup : Popup
    {
        public event Action<PlayerIconInfo> OnPlayerIconChanged;

        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _scrollContent;

        private void Start()
        {
            _returnButton.onClick.AddListener(OnClosing);
        }

        public void Initialize(PlayerIconsConfiguration playerIconsConfigurations)
        {
            foreach (var playerIconInfo in playerIconsConfigurations.PlayerIconInfos)
            {
                Button button = Instantiate(_button, _scrollContent.transform);
                button.onClick.AddListener(() => SetPlayerIcon(playerIconInfo));
                button.image.sprite = playerIconInfo.Icon;
            }
        }

        private void SetPlayerIcon(PlayerIconInfo iconInfo)
        {
            OnPlayerIconChanged?.Invoke(iconInfo);
            OnClosing();
        }


        public override void EnableInput()
        {
            _returnButton.interactable = true;
        }

        public override void DisableInput()
        {
            _returnButton.interactable = false;
        }
    }
}