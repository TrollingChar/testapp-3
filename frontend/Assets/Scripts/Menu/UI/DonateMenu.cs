using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;


namespace Menu.UI {

    public class DonateMenu : Panel {

        [SerializeField] private Button _backButton;
        
        private MenuScene MenuScene { get; set; }
        
        
        protected override void Activate () {
            MenuScene = The.MenuScene;
        }


        protected override void Deactivate () {
            _backButton.onClick.RemoveListener(OnBackClicked);
        }


        private void OnBackClicked () {
            MenuScene.ShowMainMenu();
        }

    }

}
