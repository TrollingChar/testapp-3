using Core;
using Menu;
using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class DonateMenu : Panel {

        [SerializeField] private Button _backButton;
        
        private MenuScene MenuScene { get; set; }
        
        
        protected override void Activate () {
            MenuScene = The.MenuScene;
            _backButton.onClick.AddListener(OnBackClicked);
        }


        protected override void Deactivate () {
            _backButton.onClick.RemoveListener(OnBackClicked);
        }


        private void OnBackClicked () {
            MenuScene.ShowMainMenu();
        }

    }

}
