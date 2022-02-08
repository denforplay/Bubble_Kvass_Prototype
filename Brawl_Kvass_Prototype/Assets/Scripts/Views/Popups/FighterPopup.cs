using System;
using Configurations;
using Configurations.Info;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;
using Views.Buttons;

namespace Views.Popups
{
    public class FighterPopup : Popup
    {
        public event Action<FighterInfo> OnFighterClicked;
        
        [SerializeField] private Image _background;
        [SerializeField] private RarityConfiguration _rarityConfiguration;
        [SerializeField] private Transform _scrollContent;
        [SerializeField] private FightersConfiguration _fightersConfiguration;
        [SerializeField] private FighterButton _fighterButtonPrefab;
        [SerializeField] private Button _returnButton;

        public void Initialize(Sprite backgroundSprite)
        {
            _background.sprite = backgroundSprite;
        }
        
        private void Start()
        {
            _returnButton.onClick.AddListener(OnClosing);
            
            foreach (var fighterInfo in _fightersConfiguration.FighterInfos)
            {
                var fighterButton = Instantiate(_fighterButtonPrefab, _scrollContent);
                fighterButton.FighterImage.sprite = fighterInfo.FighterSprite;
                fighterButton.FighterName.text = fighterInfo.FighterName;
                fighterButton.Button.image.color = _rarityConfiguration[fighterInfo.FighterRarity];
                fighterButton.Button.onClick.AddListener(() => OnFighterClicked?.Invoke(fighterInfo));
            }
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