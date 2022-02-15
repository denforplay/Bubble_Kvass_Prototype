using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Buttons
{
    public class TextImageButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public TextMeshProUGUI Text => _text;
        public Button Button => _button;
        public Image Image => _image;
    }
}