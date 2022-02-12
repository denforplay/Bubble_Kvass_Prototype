using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Infos/Case info", order = 0)]
    public class CaseInfo : ScriptableObject
    {
        [SerializeField] private string _caseName;
        [SerializeField] private Sprite _caseSprite;
        [SerializeField] private int _caseCost;

        public string CaseName => _caseName;
        public Sprite CaseSprite => _caseSprite;
        public int CaseCost => _caseCost;
    }
}