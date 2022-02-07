using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Inputs configuration", order = 0)]
    public class InputsConfiguration : ScriptableObject
    {
        [SerializeField] [Range(1, 10f)] private float _accelerometerSensitivity;

        public float AccelerometerSensitivity => _accelerometerSensitivity;
    }
}