using Battle.Camera;
using Battle.Generation;
using Battle.State;
using Battle.Teams;
using Core;
using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Random;
using Utils.Singleton;


namespace Battle {

    public class BattleScene : MonoBehaviour {

        [SerializeField] private Text _hint;
        [SerializeField] private SpriteRenderer _landRenderer;

        public WSConnection Connection { get; private set; }
        public CameraWrapper Camera { get; private set; }
        public GameStateController State { get; private set; }
        public TeamManager Teams { get; private set; }
        public World World { get; private set; }

        // temp fields
        private EstimatedLandGen _landGen;
        private GameInitData _initData;
        private bool _initialized;

        public BattleLoadedMessenger OnBattleLoaded { get; private set; }


        private void Awake () {
            The<BattleScene>.Set(this);
            OnBattleLoaded = new BattleLoadedMessenger();

            _initData = (GameInitData) The<SceneSwitcher>.Get().Data[0];
            RNG.Init(_initData.Seed);

            Connection = The<WSConnection>.Get();
            Connection.OnTurnData.Subscribe(Work);

            Camera = GetComponentInChildren<CameraWrapper>();
            The<CameraWrapper>.Set(Camera);

            StartLandGen();
        }


        private void StartLandGen () {
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
            State = new GameStateController();
            World = new World(gen, _landRenderer);
            Camera.LookAt(new Vector2(1000, 1000), true);
            Teams = World.SpawnTeams(_initData.Players, 5);

            OnBattleLoaded.Send();
        }


        private void FixedUpdate () {
            if (!_initialized) return;
            if (State.CurrentState == GameState.Synchronizing) return;
            if (State.CurrentState != GameState.Turn) {
                Work(null);
                return;
            }
            if (!State.IsMyTurn) return;

            // gather input and update world
            var td = new TurnData();
            Connection.SendTurnData(td);
            Work(td);
        }


        private void Work (TurnData td) {
            World.Update(td);
            State.Update();
        }


        private void OnDestroy () {
            Connection.OnTurnData.Unsubscribe(Work);
            The<World>.Set(null);
            The<GameStateController>.Set(null);
        }


        // temp method
        public void ShowHint (string text) {
            _hint.text = text;
        }

    }

}
