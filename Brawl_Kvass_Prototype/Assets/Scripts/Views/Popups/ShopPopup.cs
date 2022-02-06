using Configurations;
using Configurations.Info;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;
using Views.Buttons;

namespace Views.Popups
{
    public class ShopPopup : Popup
    {
        [SerializeField] private Transform _scrollContent;
        [SerializeField] private CaseButton _caseButtonPrefab;
        [SerializeField] private CasesConfiguration _casesConfiguration;
        [SerializeField] private Image _background;
        [SerializeField] private Button _returnButton;
        
        private void Start()
        {
            foreach (var caseInfo in _casesConfiguration.Cases)
            {
                var button = Instantiate(_caseButtonPrefab, _scrollContent);
                button.CaseName.text = caseInfo.CaseName;
                button.CaseImage.sprite = caseInfo.CaseSprite;
                button.CaseCostText.text = caseInfo.CaseCost.ToString();
                button.Button.onClick.AddListener(() => BuyCase(caseInfo));
            }
            
            _returnButton.onClick.AddListener(OnClosing);
        }

        private void BuyCase(CaseInfo caseInfo)
        {
            Debug.Log("Case opened");
        }

        public void Initialize(Sprite backgroundSprite)
        {
            _background.sprite = backgroundSprite;
        }
        
        public override void EnableInput()
        {
            _returnButton.interactable = true;
        }

        public override void DisableInput()
        {
            _returnButton.interactable = false;
        }
    }
}