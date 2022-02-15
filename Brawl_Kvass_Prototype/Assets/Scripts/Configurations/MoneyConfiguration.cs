using System.Collections.Generic;
using Configurations.Info;
using Core.Enums;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Money configuration", order = 0)]
    public class MoneyConfiguration : ScriptableObject
    {
        [SerializeField] private List<MoneyInfo> _moneyInfos;

        public Sprite this[MoneyType moneyType]
        {
            get => _moneyInfos.Find(x => x.MoneyType == moneyType).MoneySprite;
        }
    }
}