using Core;
using Core.UI;
using DataTransfer.Server;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.UI {

    public class EndGameMenu : Panel {

        [SerializeField] private Text _text;
        [SerializeField] private Button _okButton;


        protected override void Activate () {
            GameEndedCmd.OnReceived.Subscribe(OnGameEnded);
            _okButton.onClick.AddListener(OnClickedOk);
        }


        private void OnClickedOk () {
            The.SceneSwitcher.Load(Scenes.Menu, true); // bypass connection menu
        }


        protected override void Deactivate () {
            GameEndedCmd.OnReceived.Unsubscribe(OnGameEnded);
            _okButton.onClick.RemoveListener(OnClickedOk);
        }


        private void OnGameEnded (GameEndedCmd cmd) {
            switch (cmd.Result) {
                case GameEndedCmd.Draw:
                    _text.text = "Ничья";
                    break;
                case GameEndedCmd.Victory:
                    _text.text = cmd.PlayerId == The.PlayerInfo.Id ? "Победа!" : "Победил игрок " + cmd.PlayerId;
                    break;
                case GameEndedCmd.Desync:
                    _text.text = "Рассинхронизация";
                    break;
                default:
                    _text.text = "(Пришел неверный код сообщения)";
                    break;
            }
            Show();
        }

    }

}
