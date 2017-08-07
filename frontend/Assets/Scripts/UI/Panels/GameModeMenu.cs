using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace UI.Panels {

    public class GameModeMenu : Panel {

        [Inject] private MainMenu _mainMenu;

        [SerializeField] private Button _1Player, _2Players, _3Players, _backButton;

        
        protected override void Activate () {
            _1Player.onClick.AddListener(Back);
            _2Players.onClick.AddListener(Back);
            _3Players.onClick.AddListener(Back);
            _backButton.onClick.AddListener(Back);
        }


        protected override void Deactivate () {
            _1Player.onClick.RemoveListener(Back);
            _2Players.onClick.RemoveListener(Back);
            _3Players.onClick.RemoveListener(Back);
            _backButton.onClick.RemoveListener(Back);
        }


        private void Back () {
            Hide();
            _mainMenu.Show();
        }

    }

}
