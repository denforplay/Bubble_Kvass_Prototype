using System.Collections.Generic;
using Configurations.Info;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Player icons configuration", order = 0)]
    public class PlayerIconsConfiguration : ScriptableObject
    {
        [SerializeField] private List<PlayerIconInfo> _playerIconInfos;

        public List<PlayerIconInfo> PlayerIconInfos => _playerIconInfos;
    }
}