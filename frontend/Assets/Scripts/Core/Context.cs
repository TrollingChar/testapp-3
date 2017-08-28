using Battle;
using Commands.Server;
using Net;
using UnityEngine;
using Utils.Singleton;


namespace Core {

    public class Context : MonoBehaviour {

        private WSConnection _connection;
        private SceneSwitcher _sceneSwitcher;


        private void Awake () {
            DontDestroyOnLoad(this);

            _connection = gameObject.AddComponent<WSConnection>();
            _sceneSwitcher = new SceneSwitcher();

            CommandExecutor<AuthorizedCommand>.AddHandler(OnAuthorized);
            CommandExecutor<GameStartedCommand>.AddHandler(OnGameStarted);
            _sceneSwitcher.Load(Scenes.Menu);
        }


        private void OnGameStarted (GameStartedCommand cmd) {
            _sceneSwitcher.Load(Scenes.Battle, cmd.Data);
        }


        private void OnAuthorized (AuthorizedCommand cmd) {
            The<PlayerInfo>.Set(cmd.PlayerInfo);
            CommandExecutor<AuthorizedCommand>.RemoveHandler(OnAuthorized);
        }

    }

}
