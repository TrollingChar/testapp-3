using Assets;
using Net;
using UI;
using UnityEngine;
using Utils;
using War;


public class Core : MonoBehaviour {

    [SerializeField] private AssetsLoader _assets;
    [SerializeField] private GameObject _bfPrefab;

    public static WSConnection Connection;
    public static BF BF;
    public static CoreEvents CoreEvents;
    public static int Id;


    private void Start () {
        Instantiate(_assets);
        Connection = gameObject.GetComponent<WSConnection>();
        CoreEvents = gameObject.GetComponent<CoreEvents>();
    }

    public void AuthAccepted (int id) {
        Id = id;
    }


    public void GenerateWorld (GameData data) {
        RNG.Init(data.Seed);
        BF = Instantiate(_bfPrefab).GetComponent<BF>();
        BF.StartGame(data.Players);
    }


    private void FixedUpdate () {
        Connection.Work(); // receive data from server and update world
        if (BF != null) BF.Work(); // update world independently
    }


    public void UpdateWorld (TurnData td) {
        BF.Work(td);
    }


    public void NewTurn (int player) {
        BF.State.StartTurn(player);
    }


    public static void Synchronize () {
        Connection.SendEndTurn(true);
    }

}
