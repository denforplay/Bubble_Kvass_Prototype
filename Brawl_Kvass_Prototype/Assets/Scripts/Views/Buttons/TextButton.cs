using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Buttons
{
    public class TextButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        public TextMeshProUGUI Text => _text;
        public Button Button => _button;
    }
}