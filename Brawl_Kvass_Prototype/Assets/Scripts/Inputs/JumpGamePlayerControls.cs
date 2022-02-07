using Configurations;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Views;

namespace Inputs
{
    public class JumpGamePlayerControls : IInputController
    {
        private readonly InputsConfiguration _inputsConfiguration;
        private readonly PlayerConfiguration _playerConfiguration;
        private readonly Transformable2DView _characterTransformable;
        private readonly PlayerInputs _playerInputs;

        public JumpGamePlayerControls(Transformable2DView characterTransformable, PlayerConfiguration playerConfiguration, InputsConfiguration inputsConfiguration)
        {
            _characterTransformable = characterTransformable;
            _playerConfiguration = playerConfiguration;
            _inputsConfiguration = inputsConfiguration;
            _playerInputs = new PlayerInputs();
        }

        public void Update()
        {
            var direction = _playerInputs.JumpGameInputs.Movement.ReadValue<Vector3>();
            var horizontalVelocity = direction.x * _inputsConfiguration.AccelerometerSensitivity;
            _characterTransformable.SetHorizontalVelocity(horizontalVelocity * _playerConfiguration.Speed);
        }

        public void OnEnable()
        {
            if (Accelerometer.current != null)
            {
                InputSystem.EnableDevice(Accelerometer.current);
            }
            
            _playerInputs.Enable();
        }

        public void OnDisable()
        {
            _playerInputs.Disable();
        }
    }
}