﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public static Core I;

    public WSConnection connection;
    public W3.BF bf;
    public Texture2D landTexture;
    public SpriteRenderer landRenderer;
    W3.TurnData td;
    public CameraWrapper cameraWrapper;
    public Assets assets;

    void Start () {
        I = this;
    }

    public void GenerateWorld (int seed) {
        RNG.Init(seed);
        bf = new W3.BF();
    }
	
	void FixedUpdate () {
        connection.Work(); // receive data from server and update world
        if (bf != null) bf.Update(); // update world independently
	}

    public void UpdateWorld (W3.TurnData td) {
        bf.Update(td);
    }

    public void Synchronize () {
        connection.SendEndTurn(true);
    }

    public W3.Worm NextWorm () {
        return null;
    }

    public void ResetActivePlayer () {
        //
    }
}
