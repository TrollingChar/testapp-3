using System.Collections.Generic;
using UnityEngine;
using War.Camera;
using War.Objects;
using War.Physics;


namespace War {

    public class BF : MonoBehaviour {

        // land and all w3colliders:
        public World world;

        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private new UnityEngine.Camera camera;

        [HideInInspector] public CameraWrapper cameraWrapper;

        // all turn-based logic:
        public GameStateController state;

        private bool paused;


        private void Awake () {
            world = new World(Assets.Assets.motherboard, renderer);
            state = new GameStateController();
            cameraWrapper = camera.GetComponent<CameraWrapper>();
        }


        public void Work () {
            // refresh graphics and do logic if my turn
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
                //world.AddObject(new Worm(), new XY(RNG.Int(500, 1500), 1000));
            }
        }


        public void Work (TurnData td) {
            // do game logic
            world.Update(td);
            state.Update();
        }


        public Worm NextWorm () {
            return null;
        }

    }

}
