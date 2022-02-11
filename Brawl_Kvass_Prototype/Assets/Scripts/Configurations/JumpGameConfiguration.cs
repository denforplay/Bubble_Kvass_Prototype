using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Jump game configuration", order = 0)]
    public class JumpGameConfiguration : ScriptableObject
    {
        [SerializeField] private int _coinScoreNeeded;
        [SerializeField] private int _gemScoreNeeded;

        public int CoinScoreNeeded => _coinScoreNeeded;
        public int GemScoreNeeded => _gemScoreNeeded;
    }
}