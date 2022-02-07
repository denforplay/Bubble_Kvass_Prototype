using System;
using Configurations;
using Configurations.Info;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;
using Views.Buttons;

namespace Views.Popups
{
    public class MinigamesChoosePopup : Popup
    {
        public event Action<MinigameInfo> OnMiniGameClicked;
        
        [SerializeField] private MinigameButton _minigameButtonPrefab;
        [SerializeField] private MiniGamesConfiguration _gamesConfiguration;
        [SerializeField] private Transform _scrollContent;
        [SerializeField] private Image _background;
        [SerializeField] private Button _returnButton;

        public void Initialize(Sprite backgroundSprite)
        {
            _background.sprite = backgroundSprite;
        }

        private void Start()
        {
            _returnButton.onClick.AddListener(OnClosing);
            foreach (var miniGameInfo in _gamesConfiguration.MiniGamesInfos)
            {
                var miniGameButton = Instantiate(_minigameButtonPrefab, _scrollContent);
                miniGameButton.GameIcon.sprite = miniGameInfo.MiniGameIcon;
                miniGameButton.GameMiniIcon.sprite = miniGameInfo.MiniGameMiniIcon;
                miniGameButton.GameName.text = miniGameInfo.MiniGameName;
                miniGameButton.Button.onClick.AddListener(() => ChooseMiniGame(miniGameInfo));
            }
        }

        private void ChooseMiniGame(MinigameInfo minigameInfo)
        {
            OnMiniGameClicked?.Invoke(minigameInfo);
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