using Core.Enums;
using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Infos/Rarity info", order = 0)]
    public class RarityInfo : ScriptableObject
    {
        [SerializeField] private Rarity _rarity;
        [SerializeField] private Color _color;

        public Rarity Rarity => _rarity;
        public Color Color => _color;
    }
}