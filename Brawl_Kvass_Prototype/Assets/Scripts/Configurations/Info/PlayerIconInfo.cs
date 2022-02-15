using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Infos/Player icon info", order = 0)]
    public class PlayerIconInfo : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _isUnlocked;
 
        public int Id => _id;
        public Sprite Icon => _icon;
        public bool IsUnlocked => _isUnlocked;
    }
}