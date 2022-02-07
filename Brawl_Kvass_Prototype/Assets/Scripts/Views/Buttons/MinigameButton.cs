using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Buttons
{
    public class MinigameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _gameName;
        [SerializeField] private Image _gameMiniIcon;
        [SerializeField] private Image _gameIcon;

        public Button Button => _button;
        public TextMeshProUGUI GameName => _gameName;
        public Image GameMiniIcon => _gameMiniIcon;
        public Image GameIcon => _gameIcon;
    }
}