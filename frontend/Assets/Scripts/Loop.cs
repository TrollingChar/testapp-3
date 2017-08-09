using Messengers;
using Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineInternal;
using Utils.Singleton;
using War;
using Zenject;


public class Loop : MonoBehaviour {

    [Inject] private SceneSwitcher _sceneSwitcher;
    [Inject] private WSConnection _connection;
    [Inject] private StartGameMessenger _onStartGame;
    
    private GameObject _bfGameObject;


    private void Start () {
        _onStartGame.Subscribe(InitBF);
    }


    private void InitBF (GameInitData data) {
        _onStartGame.Unsubscribe(InitBF);
        _sceneSwitcher.Load(Scenes.Battle, data);
    }


    private void Update () {
        _connection.Work();
    }

}
