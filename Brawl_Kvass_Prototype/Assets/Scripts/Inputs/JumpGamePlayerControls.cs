using Core.Interfaces;
using UnityEngine;
using Views;

namespace Inputs
{
    public class JumpGamePlayerControls : IInputController
    {
        private Transformable2DView _characterTransformable;
        private PlayerInputs _playerInputs;

        public JumpGamePlayerControls(Transformable2DView characterTransformable)
        {
            _characterTransformable = characterTransformable;
            _playerInputs = new PlayerInputs();
        }

        public void Update()
        {
            var direction = _playerInputs.JumpGameInputs.Movement.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                _characterTransformable.AddVelocity(direction/15);//CONSTANT MUST BE DELETED
                Debug.Log(direction);
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