using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Inputs configuration", order = 0)]
    public class InputsConfiguration : ScriptableObject
    {
        [SerializeField] [Range(1, 10f)] private float _accelerometerSensitivity;
        [SerializeField] [Range(1, 15f)] private float _jumpHeight;

        public float AccelerometerSensitivity => _accelerometerSensitivity;
        public float JumpHeight => _jumpHeight;
    }
}