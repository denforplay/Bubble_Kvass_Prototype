using Configurations;
using Core.Interfaces;
using UnityEngine;
using Views;

namespace Inputs
{
    public class RunGamePlayerControls : IInputController
    {
        private InputsConfiguration _inputsConfiguration;
        private Transformable2DView _characterTransformable;
        private PlayerInputs _playerInputs;

        private bool _isInJumping;
        private bool _isInDoubleJumping;
        private readonly float _jumpHeight;
        
        public RunGamePlayerControls(Transformable2DView characterTransformable, InputsConfiguration inputsConfiguration)
        {
            _inputsConfiguration = inputsConfiguration;
            _jumpHeight = _inputsConfiguration.JumpHeight;
            _characterTransformable = characterTransformable;
            _playerInputs = new PlayerInputs();
            _playerInputs.RunGameInputs.Jump.started += _ =>
            {
                if (!_isInJumping)
                    _characterTransformable.SetVerticalVelocity(_jumpHeight);
                else if (!_isInDoubleJumping)
                {
                    _characterTransformable.SetVerticalVelocity(_jumpHeight);
                    _isInDoubleJumping = true;
                }
            };
        }

        public void Update()
        {
            Debug.Log(_playerInputs.RunGameInputs.Jump.triggered);
            _isInJumping = _characterTransformable.Model.Velocity.y != 0;
            if (_characterTransformable.Model.Velocity.y == 0)
                _isInDoubleJumping = false;
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