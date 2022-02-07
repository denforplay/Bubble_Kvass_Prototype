using Core.Abstracts;
using Core.PopupSystem;
using Models.Collisions;
using TMPro;
using UnityEngine;

namespace Views.Popups.MiniGamesPopups
{
    public abstract class MiniGamePopup : Popup
    {
        [SerializeField] private TextMeshProUGUI _currentPointsText;
        [SerializeField] private TextMeshProUGUI _bestPointsText;
        [SerializeField] private Transformable2DView _playingCharacter;
        [SerializeField] private CollisionEvent _characterEvent;
        private CollisionController _collisionController;

        public Transformable2DView Character => _playingCharacter;
        public CollisionEvent CharacterEvent => _characterEvent;

        public void Initialize(CollisionController collisionController, Transformable2D playingCharacter)
        {
            _collisionController = collisionController;
            _playingCharacter.Initialize(playingCharacter);
            _characterEvent.Initialize(_collisionController, _playingCharacter.Model);
            _playingCharacter.transform.parent = null;
        }

        public void SetPoints(int points)
        {
            _currentPointsText.text = $"Score: {points}";
        }

        public void SetBestPoints(int points)
        {
            _bestPointsText.text = $"Best: {points}";
        }

        private void OnDestroy()
        {
            if (_playingCharacter != null)
                Destroy(_playingCharacter.gameObject);
        }
    }
}