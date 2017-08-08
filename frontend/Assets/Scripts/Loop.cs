using Messengers;
using Net;
using UnityEngine;
using Utils.Singleton;
using War;
using Zenject;


public class Loop : MonoBehaviour {

    [Inject] private WSConnection _connection;
    [Inject] private StartGameMessenger _onStartGame;
    [Inject(Id = W3Installer.IdBF)] private GameObject _bfPrefab;

    private GameObject _bfGameObject;
    public BF BF { get; private set; }


    private void Start () {
        Debug.Log(_connection);
        Debug.Log(_onStartGame);
        Debug.Log(_bfPrefab);
        _onStartGame.Subscribe(InitBF);
    }


    private void OnDestroy () {
        _onStartGame.Unsubscribe(InitBF);
    }


    private void InitBF (GameInitData data) {
        _bfGameObject = Instantiate(_bfPrefab);
        BF = _bfGameObject.GetComponent<BF>();
        The<BF>.Set(BF);
        BF.StartGame(data);
    }


    private void FixedUpdate () {
        // todo: split logic in menu and in game
        _connection.Work();

        // todo: update bf if exists
        if (BF != null) BF.Work();
    }


    private void Update () {}

}
