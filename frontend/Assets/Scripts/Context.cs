using Net;
using Scenes;
using UnityEngine;
using Utils.Singleton;
using War;


public class Context : MonoBehaviour {

    private WSConnection _connection;
    private SceneSwitcher _sceneSwitcher;


    // todo: move to new scene, it should not call awake twice
    private void Awake () {
        DontDestroyOnLoad(this);
        _connection = gameObject.AddComponent<WSConnection>();
        _sceneSwitcher = new SceneSwitcher();
        
        _connection.OnPlayerInfoReceived.Subscribe(OnPlayerInfo);
        _connection.OnStartGame.Subscribe(OnStartGame);
    }


    private void OnStartGame (GameInitData data) {
        _sceneSwitcher.Load(Scenes.Scenes.Menu, data);
    }


    private void OnPlayerInfo (PlayerInfo playerInfo) {
        The<PlayerInfo>.Set(playerInfo);
        _connection.OnPlayerInfoReceived.Unsubscribe(OnPlayerInfo);
    }

}
