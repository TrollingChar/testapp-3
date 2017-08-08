using Assets;
using Messengers;
using Net;
using UI;
using UnityEngine;
using Utils;
using Utils.Singleton;
using War;


public class Core : MonoBehaviour {

    [SerializeField] private AssetContainer _assets;
    [SerializeField] private GameObject _bfPrefab;

    private WSConnection _connection;
    private BF _bf;
    private CoreEvents _coreEvents;

    [HideInInspector] public int Id; // todo: replace with playerinfo


    /*
    bindings:
    
    Core -> this
    WSConnection -> self
    CoreEvents -> self
    
    */


    private void Start () {
//        MessengersConfig.Configure();
        The<Core>.Set(this);
        The<WSConnection>.Set(_connection = gameObject.GetComponent<WSConnection>());
        The<CoreEvents>.Set(_coreEvents = gameObject.GetComponent<CoreEvents>());
        Instantiate(_assets);
    }


    public void AuthAccepted (int id) {
        Id = id;
    }


    public void GenerateWorld (GameInitData data) {
        RNG.Init(data.Seed);
        _bf = Instantiate(_bfPrefab).GetComponent<BF>();
        The<BF>.Set(_bf);
        _bf.StartGame(data.Players);
    }


    private void FixedUpdate () {
        The<WSConnection>.Get().Work(); // receive data from server and update world
        if (_bf != null) _bf.Work(); // update world independently
    }


    public void UpdateWorld (TurnData td) {
        _bf.Work(td);
    }


    public void NewTurn (int player) {
        _bf.State.StartTurn(player);
    }


    public void Synchronize () {
        _connection.SendEndTurn(true);
    }

}
