using System.Collections.Generic;
using UnityEngine;

namespace Core.PopupSystem.Configurations
{
        [CreateAssetMenu(menuName = "Configurations/Popup system configuration", order = 0)]
        public class PopupSystemConfig : ScriptableObject
        {
            [SerializeField] private List<Popup> _popupPrefabs;
            public List<Popup> PopupPrefabs => _popupPrefabs;
        }
}
