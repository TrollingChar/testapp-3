using Assets;
using Net;
using Scenes;
using UnityEngine;
using Utils.Singleton;
using War;


public class Context : MonoBehaviour {

    private WSConnection _connection;
    private SceneSwitcher _sceneSwitcher;


    private void Awake () {
        DontDestroyOnLoad(this);

        _connection = gameObject.AddComponent<WSConnection>();
        _sceneSwitcher = new SceneSwitcher();

        _connection.OnPlayerInfo.Subscribe(OnPlayerInfo);
        _connection.OnStartGame.Subscribe(OnStartGame);
        _sceneSwitcher.Load(Scenes.Scenes.Menu);
    }


    private void OnStartGame (GameInitData data) {
        _sceneSwitcher.Load(Scenes.Scenes.Battle, data);
    }


    private void OnPlayerInfo (PlayerInfo playerInfo) {
        The<PlayerInfo>.Set(playerInfo);
        _connection.OnPlayerInfo.Unsubscribe(OnPlayerInfo);
    }

}
