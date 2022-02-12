using Core.Enums;
using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Infos/Money type info", order = 0)]
    public class MoneyInfo : ScriptableObject
    {
        [SerializeField] private Sprite _moneySprite;
        [SerializeField] private MoneyType _moneyType;

        public Sprite MoneySprite => _moneySprite;
        public MoneyType MoneyType => _moneyType;
    }
}