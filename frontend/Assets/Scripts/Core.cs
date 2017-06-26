using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public AssetsLoader assets;
    public GameObject bfPrefab;

    public static WSConnection connection;
    public static W3.BF bf;
    public static CoreEvents coreEvents;
    public static int id;

    void Start () {
        Instantiate(assets);
        connection = gameObject.GetComponent<WSConnection>();
        coreEvents = gameObject.GetComponent<CoreEvents>();
    }

    public void AuthAccepted (int id) {
        Core.id = id;
    }

    public void GenerateWorld (W3.GameData data) {
        RNG.Init(data.seed);
        bf = Instantiate(bfPrefab).GetComponent<W3.BF>();
        bf.StartGame(data.players);
    }
	
	void FixedUpdate () {
        connection.Work(); // receive data from server and update world
        if (bf != null) bf.Work(); // update world independently
	}

    public void UpdateWorld (W3.TurnData td) {
        bf.Work(td);
    }

    public void NewTurn (int player) {
        bf.state.StartTurn(player);
    }

    public static void Synchronize () {
        connection.SendEndTurn(true);
    }
}
