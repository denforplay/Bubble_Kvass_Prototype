using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Buttons
{
    public class FighterButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _fighterImage;
        [SerializeField] private TextMeshProUGUI _fighterName;

        public Button Button => _button;
        public Image FighterImage => _fighterImage;
        public TextMeshProUGUI FighterName => _fighterName;
    }
}