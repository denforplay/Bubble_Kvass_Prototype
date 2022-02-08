using Core.Interfaces;
using UnityEngine;
using Views;
using Zenject;

namespace Inputs
{
    public class RunGamePlayerControls : IInputController
    {
        private Transformable2DView _characterTransformable;
        private PlayerInputs _playerInputs;

        private bool IsInJumping;
        private bool IsInDoubleJumping;
        
        public RunGamePlayerControls(Transformable2DView characterTransformable)
        {
            _characterTransformable = characterTransformable;
            _playerInputs = new PlayerInputs();
            _playerInputs.RunGameInputs.Jump.started += _ =>
            {
                if (!IsInJumping)
                    _characterTransformable.SetVerticalVelocity(10);
                else if (!IsInDoubleJumping)
                {
                    _characterTransformable.SetVerticalVelocity(10);
                    IsInDoubleJumping = true;
                }
            };
        }

        public void Update()
        {
            Debug.Log(_playerInputs.RunGameInputs.Jump.triggered);
            IsInJumping = _characterTransformable.Model.Velocity.y != 0;
            if (_characterTransformable.Model.Velocity.y == 0)
                IsInDoubleJumping = false;
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