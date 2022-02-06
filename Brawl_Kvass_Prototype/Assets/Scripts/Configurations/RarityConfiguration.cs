using System.Collections.Generic;
using Configurations.Info;
using Core.Enums;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Rarity configuration", order = 0)]
    public class RarityConfiguration : ScriptableObject
    {
        [SerializeField] private List<RarityInfo> _rarityInfos;

        public Color this[Rarity rarity]
        {
            get => _rarityInfos.Find(x => x.Rarity == rarity).Color;
        }
    }
}