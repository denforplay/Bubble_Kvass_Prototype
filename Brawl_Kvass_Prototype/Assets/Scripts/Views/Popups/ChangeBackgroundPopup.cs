using System;
using Configurations;
using Configurations.Info;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;
using Views.Buttons;

namespace Views.Popups
{
    public class ChangeBackgroundPopup : Popup
    {
        public event Action<BackgroundInfo> OnBackgroundChanged;
        [SerializeField] private Image _background;
        [SerializeField] private TextImageButton _backgroundImageButtonPrefab;
        [SerializeField] private MoneyConfiguration _moneyConfiguration;
        [SerializeField] private MainMenuBackgroundConfiguration _backgroundsConfiguration;
        [SerializeField] private GameObject _scrollContent;
        [SerializeField] private Button _returnButton;
        private void Start()
        {
            _returnButton.onClick.AddListener(OnClosing);
            foreach (var background in _backgroundsConfiguration.Backgrounds)
            {
                TextImageButton imageButton = Instantiate(_backgroundImageButtonPrefab, _scrollContent.transform);
                imageButton.Button.image.sprite = background.Sprite;
                if (background.Cost == 0)
                {
                    imageButton.Button.onClick.AddListener(() => SetBackground(background));
                    imageButton.Image.color = Color.clear;
                }
                else
                {
                    imageButton.Text.text = background.Cost.ToString();
                    imageButton.Image.sprite = _moneyConfiguration[background.MoneyType];
                }
            }
        }

        public void Initialize(Sprite backgroundSprite)
        {
            _background.sprite = backgroundSprite;
        }

        private void SetBackground(BackgroundInfo background)
        {
            OnBackgroundChanged?.Invoke(background);
            OnClosing();
        }
        
        public override void EnableInput()
        {
        }

        public override void DisableInput()
        {
        }
    }
}