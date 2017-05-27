using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public class BF {
        // land and all w3colliders:
        public World world;

        // all turn-based logic:
        public GameStateController state;

        CameraWrapper camera;

        bool paused;

        bool myTurn = false;

        public BF () {
            world = new World();
            state = new GameStateController();
            camera = Core.I.cameraWrapper;
            camera.LookAt(Vector2.zero);
        }

        public void Update () { // refresh graphics and do logic if my turn
            if (myTurn && state.currentState == GameState.Turn) {
                // gather input and update world
                var td = new TurnData();
                Core.I.connection.SendTurnData(td);
                Update(td);
            } else if (state.currentState != GameState.Synchronizing) {
                Update(null);
            }
        }

        public void Update (TurnData td) { // do game logic
            world.Update(td);
            state.Update();
            //camera.Update();
        }
    }
}