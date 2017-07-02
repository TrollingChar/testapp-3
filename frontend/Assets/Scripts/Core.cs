using Assets;
using Net;
using UI;
using UnityEngine;
using Utils;
using War;


public class Core : MonoBehaviour {

    public AssetsLoader assets;
    public GameObject bfPrefab;

    public static WSConnection connection;
    public static BF bf;
    public static CoreEvents coreEvents;
    public static int id;


    private void Start () {
        Instantiate(assets);
        connection = gameObject.GetComponent<WSConnection>();
        coreEvents = gameObject.GetComponent<CoreEvents>();
    }


    public void AuthAccepted (int id) {
        Core.id = id;
    }


    public void GenerateWorld (GameData data) {
        RNG.Init(data.seed);
        bf = Instantiate(bfPrefab).GetComponent<BF>();
        bf.StartGame(data.players);
    }


    private void FixedUpdate () {
        connection.Work(); // receive data from server and update world
        if (bf != null) bf.Work(); // update world independently
    }


    public void UpdateWorld (TurnData td) {
        bf.Work(td);
    }


    public void NewTurn (int player) {
        bf.state.StartTurn(player);
    }


    public static void Synchronize () {
        connection.SendEndTurn(true);
    }

}
