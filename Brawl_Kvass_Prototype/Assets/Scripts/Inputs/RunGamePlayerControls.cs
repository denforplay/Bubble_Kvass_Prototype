using Core.Interfaces;
using Views;

namespace Inputs
{
    public class RunGamePlayerControls : IInputController
    {
        private Transformable2DView _characterTransformable;
        private PlayerInputs _playerInputs;

        private bool IsInJumping;
        
        public RunGamePlayerControls(Transformable2DView characterTransformable)
        {
            _characterTransformable = characterTransformable;
            _playerInputs = new PlayerInputs();
            _playerInputs.RunGameInputs.Jump.performed += _ =>
            {
                if (!IsInJumping)
                    _characterTransformable.SetVerticalVelocity(10);
            };
        }

        public void Update()
        {
            IsInJumping = _characterTransformable.Model.Velocity.y != 0;
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