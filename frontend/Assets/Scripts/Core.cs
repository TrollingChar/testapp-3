using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public AssetsLoader assets;
    public GameObject bfPrefab;

    public static WSConnection connection;
    public static W3.BF bf;

    void Start () {
        Instantiate(assets);
        connection = gameObject.GetComponent<WSConnection>();
    }

    public void GenerateWorld (int seed) {
        RNG.Init(seed);
        bf = Instantiate(bfPrefab).GetComponent<W3.BF>();
        bf.world.StartGame();
    }
	
	void FixedUpdate () {
        connection.Work(); // receive data from server and update world
        if (bf != null) bf.Work(); // update world independently
	}

    public void UpdateWorld (W3.TurnData td) {
        bf.Work(td);
    }

    public void Synchronize () {
        connection.SendEndTurn(true);
    }
}
