using System;
using Core.Abstracts;
using Core.PopupSystem;
using Models.Collisions;
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
        [SerializeField] private CollisionEvent _characterEvent;
        [SerializeField] private SpriteRenderer _characterSprite;
        private CollisionController _collisionController;

        public Transformable2DView Character => _playingCharacter;

        private void Start()
        {
            _pauseButton.onClick.AddListener(() => OnPauseClicked?.Invoke());
        }

        public MiniGamePopup Initialize(CollisionController collisionController, Transformable2D playingCharacter)
        {
            _collisionController = collisionController;
            _playingCharacter.Initialize(playingCharacter);
            _characterEvent.Initialize(_collisionController, _playingCharacter.Model);
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