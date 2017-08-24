using Battle;
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

            _connection.OnPlayerInfo.Subscribe(OnPlayerInfo);
            _connection.OnStartGame.Subscribe(OnStartGame);
            _sceneSwitcher.Load(Scenes.Menu);
        }


        private void OnStartGame (GameInitData data) {
            _sceneSwitcher.Load(Scenes.Battle, data);
        }


        private void OnPlayerInfo (PlayerInfo playerInfo) {
            The<PlayerInfo>.Set(playerInfo);
            _connection.OnPlayerInfo.Unsubscribe(OnPlayerInfo);
        }

    }

}
