using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.code.bf;

static class BF {
    static bool active = false;
    static World world = null;
    static GameStateController state = null;
    static CameraController camera = null;

    static void Start (GameData data, Texture2D tex, ref Sprite landSprite) {
        RNG.Init(data.seed);
        world = new World();
        state = new GameStateController();
        camera = new CameraController();
        active = true;
    }

    static void Update (TurnData data) {
        if (!active) return;
        world.Work(data);
        state.Update();
        //camera.Work();
    }
}
