using System.Collections.Generic;
using Configurations.Info;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Fighters configuration", order = 0)]
    public class FightersConfiguration : ScriptableObject
    {
        [SerializeField] private List<FighterInfo> _fighterInfos;

        public List<FighterInfo> FighterInfos => _fighterInfos;
    }
}