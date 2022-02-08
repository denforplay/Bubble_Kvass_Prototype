using System.Collections.Generic;
using Configurations.Info;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Mini games configuration", order = 0)]
    public class MiniGamesConfiguration : ScriptableObject
    {
        [SerializeField] private List<MinigameInfo> _miniGamesInfos;

        public List<MinigameInfo> MiniGamesInfos => _miniGamesInfos;
    }
}