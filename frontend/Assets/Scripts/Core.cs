using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public static Core I;

    public WSConnection connection;
    public BF bf;
    public Texture2D landTexture;
    public SpriteRenderer landRenderer;
    TurnData td;
    public CameraWrapper cameraWrapper;

    void Start () {
        I = this;
    }

    public void GenerateWorld (int seed) {
        RNG.Init(seed);
        bf = new BF();
    }
	
	void Update () {
        connection.Work(); // receive data from server and update world
        if (bf != null) bf.Update(); // update world independently
	}

    public void UpdateWorld (TurnData td) {
        bf.Update(td);
    }

    public void Synchronize () {
        connection.SendEndTurn(true);
    }

    public Worm NextWorm () {
        return null;
    }

    public void ResetActivePlayer () {
        //
    }
}
