using Core.Abstracts;
using Core.PopupSystem;
using Models.Collisions;
using UnityEngine;

namespace Views.Popups.MiniGamesPopups
{
    public abstract class MiniGamePopup : Popup
    {
        [SerializeField] private Transformable2DView _playingCharacter;
        [SerializeField] private CollisionEvent _characterEvent;
        protected CollisionController _collisionController;

        public Transformable2DView Character => _playingCharacter;
        public CollisionEvent CharacterEvent => _characterEvent;

        public void Initialize(CollisionController collisionController, Transformable2D playingCharacter)
        {
            _collisionController = collisionController;
            _playingCharacter.Initialize(playingCharacter);
            _characterEvent.Initialize(_collisionController, _playingCharacter.Model);
        }
    }
}