using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Player configuration", order = 0)]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}