using System;
using Configurations.Info;
using Core.PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups
{
    public class FighterDescriptionPopup : Popup
    {
        public event Action<FighterInfo> OnChooseFighter;

        [SerializeField] private Image _background;
        [SerializeField] private Image _fighterImage;
        [SerializeField] private TextMeshProUGUI _fighterName;
        [SerializeField] private TextMeshProUGUI _fighterDescription;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _chooseButton;
        [SerializeField] private Button _skinButton;

        private FighterInfo _fighterInfo;

        public void Initialize(FighterInfo fighterInfo, Sprite backgroundSprite = null)
        {
            _background.sprite = backgroundSprite;
            _fighterInfo = fighterInfo;
            _fighterImage.sprite = fighterInfo.FighterSprite;
            _fighterName.text = fighterInfo.FighterName;
            _fighterDescription.text = fighterInfo.FighterDescription;
        }
        
        private void Start()
        {
            _chooseButton.onClick.AddListener(() => OnChooseFighter?.Invoke(_fighterInfo));
            _returnButton.onClick.AddListener(OnClosing);
        }

        public override void EnableInput()
        {
            _returnButton.interactable = true;
            _chooseButton.interactable = true;
            _skinButton.interactable = true;
        }

        public override void DisableInput()
        {
            _returnButton.interactable = false;
            _chooseButton.interactable = false;
            _skinButton.interactable = false;
        }
    }
}