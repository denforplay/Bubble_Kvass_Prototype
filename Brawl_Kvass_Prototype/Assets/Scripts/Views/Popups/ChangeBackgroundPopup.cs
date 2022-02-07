﻿using System;
using Configurations;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;
using Views.Buttons;

namespace Views.Popups
{
    public class ChangeBackgroundPopup : Popup
    {
        public event Action<Sprite> OnBackgroundChanged;
        [SerializeField] private Image _background;
        [SerializeField] private TextButton _backgroundButtonPrefab;
        [SerializeField] private MainMenuBackgroundConfiguration _backgroundsConfiguration;
        [SerializeField] private GameObject _scrollContent;
        [SerializeField] private Button _returnButton;

        private void Start()
        {
            _returnButton.onClick.AddListener(OnClosing);
            foreach (var background in _backgroundsConfiguration.Backgrounds)
            {
                TextButton button = Instantiate(_backgroundButtonPrefab, _scrollContent.transform);
                button.Button.image.sprite = background.Sprite;
                if (background.Cost == 0)
                {
                    button.Button.onClick.AddListener(() => SetBackground(background.Sprite));
                }
                else
                {
                    button.Text.text = background.Cost.ToString();
                }
            }
        }

        public void Initialize(Sprite backgroundSprite)
        {
            _background.sprite = backgroundSprite;
        }

        private void SetBackground(Sprite sprite)
        {
            OnBackgroundChanged?.Invoke(sprite);
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