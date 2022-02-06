using Configurations;
using Core.Interfaces;
using UnityEngine;
using Views;

namespace Inputs
{
    public class JumpGamePlayerControls : IInputController
    {
        private PlayerConfiguration _playerConfiguration;
        private Transformable2DView _characterTransformable;
        private PlayerInputs _playerInputs;

        public JumpGamePlayerControls(Transformable2DView characterTransformable, PlayerConfiguration playerConfiguration)
        {
            _characterTransformable = characterTransformable;
            _playerConfiguration = playerConfiguration;
            _playerInputs = new PlayerInputs();
        }

        public void Update()
        {
            var direction = _playerInputs.JumpGameInputs.Movement.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                _characterTransformable.SetHorizontalVelocity(direction.x * _playerConfiguration.Speed);
                Debug.Log(direction);
            }
            else
            {
                _characterTransformable.SetHorizontalVelocity(0);
            }
        }

        public void OnEnable()
        {
            _playerInputs.Enable();
        }

        public void OnDisable()
        {
            _playerInputs.Disable();
        }
    }
}