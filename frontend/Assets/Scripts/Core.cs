using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public WSConnection connection;
    World world;
    public Texture2D landTexture;
    public SpriteRenderer landRenderer;
    TurnData td;
    public CameraWrapper cameraWrapper;

    void Start () { }

    public void GenerateWorld (int seed) {
        RNG.Init(seed);
        world = new World(this);
    }
	
	void Update () {
        connection.Work();
        if (world != null) world.Update();
	}

    public void UpdateWorld (TurnData td) {
        world.Update(td);
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
