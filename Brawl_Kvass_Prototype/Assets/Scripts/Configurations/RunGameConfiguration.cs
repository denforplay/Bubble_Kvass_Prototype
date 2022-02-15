using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Run game configuration", order = 0)]
    public class RunGameConfiguration : ScriptableObject
    {
        [SerializeField] private float _minBarrierSpeed;
        [SerializeField] private float _maxBarrierSpeed;
        [SerializeField] private int _coinScoreNeeded;
        [SerializeField] private int _gemScoreNeeded;
        
        public float MinBarrierSpeed => _minBarrierSpeed;
        public float MaxBarrierSpeed => _maxBarrierSpeed;
        public int CoinScoreNeeded => _coinScoreNeeded;
        public int GemScoreNeeded => _gemScoreNeeded;
    }
}