using System;
using Battle.Camera;
using Battle.Generation;
using Battle.State;
using Battle.Teams;
using Battle.UI;
using Commands.Client;
using Commands.Server;
using Core;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Messenger;
using Utils.Random;
using Utils.Singleton;


namespace Battle {

    public class BattleScene : MonoBehaviour {

        [SerializeField] private Text _hint;
        [SerializeField] private SpriteRenderer _landRenderer;

        public Connection Connection { get; private set; }
        public CameraWrapper Camera { get; private set; }
        public GameStateController State { get; private set; }
        public WeaponWrapper Weapon { get; private set; }
        public TimerWrapper Timer { get; private set; }
        public ActiveWormWrapper ActiveWorm { get; private set; }
        
        public TeamManager Teams { get; private set; }
        public World World { get; private set; }
        public ArsenalPanel ArsenalPanel { get; private set; }

        // temp fields
        private EstimatedLandGen _landGen;

        private GameInitData _initData;
        private bool _initialized;

        public readonly Messenger OnBattleLoaded = new Messenger();


        private void Awake () {
            The<BattleScene>.Set(this);

            _initData = (GameInitData) The<SceneSwitcher>.Get().Data[0];
            RNG.Init(_initData.Seed);

            Connection = The<Connection>.Get();
            CommandExecutor<HandleTurnDataCmd>.AddHandler(TurnDataHandler);

            Camera = GetComponentInChildren<CameraWrapper>();
            The<CameraWrapper>.Set(Camera);

            ArsenalPanel = GetComponentInChildren<ArsenalPanel>();
            The<ArsenalPanel>.Set(ArsenalPanel);

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


        private void TurnDataHandler (HandleTurnDataCmd cmd) {
            Work(cmd.Data);
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
            Timer = new TimerWrapper();
            World = new World(gen, _landRenderer);
            ActiveWorm = new ActiveWormWrapper();
            Weapon = new WeaponWrapper();
            Camera.LookAt(new Vector2(1000, 1000), true);
            Teams = World.SpawnTeams(_initData.Players, 5);
            
            CommandExecutor<StartNewTurnCmd>.AddHandler(PrepareTurn);
            
            OnBattleLoaded.Send();
            Timer.Time = 500;
        }


        private void FixedUpdate () {
            if (!_initialized) return;
            if (State.CurrentState == GameState.Synchronizing) return;
            if (State.CurrentState != GameState.Turn) {
                Work(null);
                return;
            }
//            if (!State.IsMyTurn) return;
            if (!Teams.IsMyTurn) return;
            
            // gather input and update world
            var td = new TurnData();
            new SendTurnDataCmd(td).Send();
            Work(td);
        }


        private void Work (TurnData td) {
            Weapon.Update(td);
            World.Update(td);
            Timer.Update();
            if (Timer.HasElapsed) State.ChangeState();
        }


        private void OnDestroy () {
            CommandExecutor<HandleTurnDataCmd>.RemoveHandler(TurnDataHandler);
            The<World>.Set(null);
            The<GameStateController>.Set(null);
        }


        [Obsolete]
        public void ShowHint (string text) {
            _hint.text = text;
        }


        public void BeforeTurn()
        {
            // drop crates
        }

        public void Synchronize()
        {
            Connection.Send(new EndTurnCmd(true));
        }
        
        private void PrepareTurn(StartNewTurnCmd cmd)
        {
            Teams.SetActive(cmd.Player);
            State.ChangeState();
        }

        public void NewTurn()
        {
            ActiveWorm.Worm = Teams.NextWorm();
            ActiveWorm.CanMove = true;
            Camera.LookAt(ActiveWorm.Worm.Position);
            Weapon.Reset();
            Timer.Time = 10000;
        }

        public void EndTurn()
        {
            ActiveWorm.CanMove = false;
            Weapon.Unequip();
            Timer.Wait(500);
        }

        public void AfterTurn()
        {
            // poison damage
        }

        public void Remove0Hp()
        {
            // remove worms with 0 hp
        }
    }

}
