using System.Collections.Generic;
using Configurations.Info;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Cases configuration", order = 0)]
    public class CasesConfiguration : ScriptableObject
    {
        [SerializeField] private List<CaseInfo> _cases;

        public List<CaseInfo> Cases => _cases;
    }
}