using System;
using Core.Abstracts;
using Core.PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Popups.MiniGamesPopups
{
    public abstract class MiniGamePopup : Popup
    {
        public event Action OnPauseClicked;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _currentPointsText;
        [SerializeField] private TextMeshProUGUI _bestPointsText;
        [SerializeField] private Transformable2DView _playingCharacter;
        [SerializeField] private SpriteRenderer _characterSprite;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _gemsText;

        public Transformable2DView Character => _playingCharacter;

        private void Start()
        {
            _pauseButton.onClick.AddListener(() => OnPauseClicked?.Invoke());
        }

        public MiniGamePopup Initialize(Transformable2D playingCharacter)
        {
            _playingCharacter.Initialize(playingCharacter);
            _playingCharacter.transform.parent = null;
            return this;
        }

        public MiniGamePopup SetCharacterSprite(Sprite characterSprite)
        {
            _characterSprite.sprite = characterSprite;
            return this;
        }

        public void SetPoints(int points)
        {
            _currentPointsText.text = $"Score: {points}";
        }

        public void SetBestPoints(int points)
        {
            _bestPointsText.text = $"Best: {points}";
        }

        public void SetCoinsText(int money)
        {
            _coinsText.text = money.ToString();
        }

        public void SetGemsText(int gems)
        {
            _gemsText.text = gems.ToString();
        }

        public override void EnableInput()
        {
            _pauseButton.interactable = true;
        }

        public override void DisableInput()
        {
            _pauseButton.interactable = false;
        }

        private void OnDestroy()
        {
            if (_playingCharacter != null)
                Destroy(_playingCharacter.gameObject);
        }
    }
}