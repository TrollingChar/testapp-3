using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public class BF : MonoBehaviour {
        new public SpriteRenderer renderer;

        // land and all w3colliders:
        public World world;
        new public Camera camera;
        [HideInInspector] new public CameraWrapper cameraWrapper;

        // all turn-based logic:
        public GameStateController state;
        bool paused;

        void Awake () {
            world = new World(Assets.motherboard, renderer);
            state = new GameStateController();
            cameraWrapper = camera.GetComponent<CameraWrapper>();
        }

        public void Work () { // refresh graphics and do logic if my turn
            if (state.activePlayer == Core.id && state.currentState == GameState.Turn) {
                // gather input and update world
                var td = new TurnData();
                Core.connection.SendTurnData(td);
                Work(td);
            } else if (state.currentState != GameState.Synchronizing) {
                Work(null);
            }
        }

        public void StartGame (List<int> players) {
            //world.AddObject(worm = new Worm(), new XY(1000, 1100));
            cameraWrapper.LookAt(new Vector2(1000, 1000), true);
            foreach (int p in players) {
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
                world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
            }
        }

        public void Work (TurnData td) { // do game logic
            world.Update(td);
            state.Update();
        }

        public Worm NextWorm () {
            return null;
        }
    }
}