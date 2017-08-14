using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Singleton;
using War;
using War.Camera;
using War.Generation;
using War.Teams;


namespace Scenes {

    public class BattleScene : MonoBehaviour {

        private CameraWrapper _camera;

        private WSConnection _connection;

        [SerializeField] private Text _hint;
        private GameInitData _initData;
        private bool _initialized;
        private EstimatedLandGen _landGen;
        [SerializeField] private SpriteRenderer _landRenderer;
        private GameStateController _state;
        private TeamManager _teams;
        private World _world;

        public BattleLoadedMessenger OnBattleLoaded { get; private set; }

        
        private void Awake () {
            The<BattleScene>.Set(this);
            OnBattleLoaded = new BattleLoadedMessenger();
            
            _initData = (GameInitData) The<SceneSwitcher>.Get().Data[0];
            RNG.Init(_initData.Seed);
            _connection = The<WSConnection>.Get();
            
            _camera = GetComponentInChildren<CameraWrapper>();
            The<CameraWrapper>.Set(_camera);

            _landGen =
                new EstimatedLandGen(
                    new LandGen(
                        new byte[,] {
                            {0, 0, 0, 0, 0},
                            {0, 1, 1, 1, 0},
                            {0, 1, 0, 1, 0}
                        }
                    )
                ).SwitchDimensions()
                .Expand(7)
                .Cellular(0x01e801d0, 20)
                .Cellular(0x01f001e0)
                .Expand()
                .Cellular(0x01e801d0, 20)
                .Cellular(0x01f001e0)
                .Rescale(2000, 1000)
                .Cellular(0x01f001e0);
            _landGen.OnProgress.Subscribe(OnProgress);
            _landGen.OnComplete.Subscribe(OnComplete); // maybe the world should handle this?
            _landGen.Generate(this);
        }


        private void OnProgress (float progress) {
            _hint.text = "Прогресс генерации: " + (int) progress + "%";
        }


        private void OnComplete (LandGen gen) {
            _initialized = true;

            _landGen.OnProgress.Unsubscribe(OnProgress);
            _landGen.OnComplete.Unsubscribe(OnComplete);

            // show ground, spawn worms
            _state = new GameStateController();
            _world = new World(gen, _landRenderer);
            _camera.LookAt(new Vector2(1000, 1000), true);
            _teams = _world.SpawnTeams(_initData.Players, 5);
            
            OnBattleLoaded.Send();
        }


        private void FixedUpdate () {
            if (!_initialized) return;

            if (_state.IsMyTurn) {
                // gather input and update world
                var td = new TurnData();
                _connection.SendTurnData(td);
                Work(td);
            } else if (_state.CurrentStates != GameStates.Synchronizing) {
                Work(null);
            }
        }


        private void Work (TurnData td) {
            _world.Update(td);
            _state.Update();
        }


        private void OnDestroy () {
            The<World>.Set(null);
            The<GameStateController>.Set(null);
        }

    }

}
