using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Buttons
{
    public class CaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _caseName;
        [SerializeField] private Image _caseImage;
        [SerializeField] private TextMeshProUGUI _caseCostText;
        [SerializeField] private Image _valutaImage;

        public Button Button => _button;
        public TextMeshProUGUI CaseName => _caseName;
        public Image CaseImage => _caseImage;
        public TextMeshProUGUI CaseCostText => _caseCostText;
        public Image ValutaImage => _valutaImage;
    }
}