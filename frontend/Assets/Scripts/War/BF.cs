using System.Collections.Generic;
using Installers;
using Net;
using UnityEngine;
using Utils;
using War.Camera;
using War.Teams;
using Zenject;
using UnObject = UnityEngine.Object;


namespace War {

    public class BF {

//} : MonoBehaviour {

        [Inject] private WSConnection _connection;
        [Inject(Id = Injectables.Id)] private int _id;
//        [Inject] private GameObject _bfPrefab;
        [Inject] private CameraWrapper _cameraWrapper;

        [Inject] private GameObject _bfGameObject;
        private MonoBehaviour _coroutineContainer;

        public World World; // land and all w3colliders:

//        public CameraWrapper CameraWrapper;
        public GameStateController State; // all turn-based logic

        public Dictionary<int, Team> Teams;
//        private bool _paused;


        public void StartGame (GameInitData data) {

//            The<BF>.Set(this);
            RNG.Init(data.Seed);

//            _bfGameObject = UnObject.Instantiate(_bfPrefab);
//            CameraWrapper = _bfGameObject.GetComponentInChildren<CameraWrapper>();

            // gen world
            GenerateWorld();
            State = new GameStateController();

            _cameraWrapper.LookAt(new Vector2(1000, 1000), true);
            Teams = World.SpawnTeams(data.Players, 5);
        }


        private void GenerateWorld () {
            World = new World(); //_bfGameObject.GetComponentInChildren<SpriteRenderer>());
        }


        public void Update () {
            return;
            // refresh graphics and do logic if my turn
            if (State.IsMyTurn) {
                // gather input and update world
                var td = new TurnData();
                _connection.SendTurnData(td);
                Update(td);
            } else if (State.CurrentState != GameState.Synchronizing) {
                Update(null);
            }
        }


        public void Update (TurnData td) {
            // do game logic
            return;
            World.Update(td);
            State.Update();
        }

    }

}
