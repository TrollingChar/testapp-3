using System.Collections.Generic;
using Net;
using UnityEngine;
using Utils.Singleton;
using War.Camera;
using War.Objects;
using War.Physics;
using War.Teams;


namespace War {

    public class BF : MonoBehaviour {

        // land and all w3colliders:
        public World World;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private UnityEngine.Camera _camera;

        [HideInInspector] public CameraWrapper CameraWrapper;

        // all turn-based logic:
        public GameStateController State;

        public Dictionary<int, Team> Teams;

        private bool _paused;

        private readonly Core _core = The<Core>.Get();
        private readonly WSConnection _connection = The<WSConnection>.Get();


        // todo: bf should not inherit from monobehavior, but incapsulate it
        private void Awake () {
            The<BF>.Set(this);
            World = new World(Assets.Assets.Motherboard, _renderer);
            State = new GameStateController();
            CameraWrapper = _camera.GetComponent<CameraWrapper>();
        }


        public void Work () {
            // refresh graphics and do logic if my turn
            if (State.ActivePlayer == _core.Id && State.CurrentState == GameState.Turn) {
                // gather input and update world
                var td = new TurnData();
                _connection.SendTurnData(td);
                Work(td);
            } else if (State.CurrentState != GameState.Synchronizing) {
                Work(null);
            }
        }


        public void StartGame (List<int> players) {
            //world.AddObject(worm = new Worm(), new XY(1000, 1100));
            CameraWrapper.LookAt(new Vector2(1000, 1000), true);
            Teams = World.SpawnTeams(players, 5);
        }


        public void Work (TurnData td) {
            // do game logic
            World.Update(td);
            State.Update();
        }

    }

}
