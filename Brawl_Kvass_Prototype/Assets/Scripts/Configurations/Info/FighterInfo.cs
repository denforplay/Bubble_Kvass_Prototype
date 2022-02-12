using Core.Enums;
using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Infos/Fighter info", order = 0)]
    public class FighterInfo : ScriptableObject
    {
        [SerializeField] public int _id;
        [SerializeField] private Sprite _fighterSprite;
        [SerializeField] private string _fighterName;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private string _fighterDescription;
        [SerializeField] private bool _isUnlocked;

        public int Id => _id;
        public Sprite FighterSprite => _fighterSprite;
        public string FighterName => _fighterName;
        public Rarity FighterRarity => _rarity;
        public string FighterDescription => _fighterDescription;
        public bool IsUnlocked => _isUnlocked;
    }
}