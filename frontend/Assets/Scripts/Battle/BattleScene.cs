﻿using System;
using Battle.Camera;
using Battle.Generation;
using Battle.Physics;
using Battle.State;
using Battle.Teams;
using Battle.UI;
using Core;
using DataTransfer.Client;
using DataTransfer.Data;
using DataTransfer.Server;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Messenger;
using Utils.Random;
using Time = Core.Time;


namespace Battle {

    public class BattleScene : MonoBehaviour {

        public readonly Messenger OnBattleLoaded = new Messenger();

        [SerializeField] private Text _hint;

        private GameInitData _initData;
        private bool _initialized;

        // temp fields
        private EstimatedLandGen _landGen;

        [SerializeField] private LandRenderer _landRenderer;

        public Connection Connection { get; private set; }
        public CameraWrapper Camera { get; private set; }
        public GameStateController State { get; private set; }
        public WeaponWrapper Weapon { get; private set; }
        public TimerWrapper Timer { get; private set; }
        public ActiveWorm ActiveWorm { get; private set; }
        public TeamManager Teams { get; private set; }
        public World World { get; private set; }
        public ArsenalPanel ArsenalPanel { get; private set; }


        private void Awake () {
            The.BattleScene = this;

            _initData = (GameInitData) The.SceneSwitcher.Data[0];
            RNG.Init(_initData.Seed);

            Connection = The.Connection;
            The.Camera = Camera = GetComponentInChildren<CameraWrapper>();
            The.ArsenalPanel = ArsenalPanel = GetComponentInChildren<ArsenalPanel>();

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
                    )
                    .SwitchDimensions()
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


        private void TurnDataHandler (TurnDataSCmd cmd) {
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
            ActiveWorm = new ActiveWorm();
            Weapon = new WeaponWrapper();
            Camera.LookAt(new Vector2(1000, 1000), true);
            Teams = World.SpawnTeams(_initData.Players, 10);

            NewTurnCmd.OnReceived.Subscribe(PrepareTurn);
            TurnDataSCmd.OnReceived.Subscribe(TurnDataHandler);

            OnBattleLoaded.Send();
            Timer.Seconds = 0.5f;
        }


        private void FixedUpdate () {
            if (!_initialized) return;
            if (State.CurrentState == GameState.Synchronizing) return;
            if (State.CurrentState != GameState.Turn) {
                Work(null);
                return;
            }
            if (!Teams.IsMyTurn) return;

            // gather input and update world
            var td = TurnData.FromInput();
            Connection.Send(new TurnDataCCmd(td));
            Work(td);
        }


        private void Work (TurnData td) {
            Weapon.Update(td);
            World.Update(td);
            Timer.Update();
            if (Timer.HasElapsed) State.ChangeState();
        }


        private void OnDestroy () {
            NewTurnCmd.OnReceived.Unsubscribe(PrepareTurn);
            TurnDataSCmd.OnReceived.Unsubscribe(TurnDataHandler);
            The.World = null;
            The.GameState = null;
        }


        [Obsolete]
        public void ShowHint (string text) {
            _hint.text = text;
        }


        public void BeforeTurn () {
            // drop crates
        }


        public void Synchronize () {
            Connection.Send(new TurnEndedCmd(true));
        }


        private void PrepareTurn (NewTurnCmd cmd) {
            Teams.SetActive(cmd.Player);
            State.ChangeState();
        }


        public void NewTurn () {
            ActiveWorm.Worm = Teams.NextWorm();
            ActiveWorm.CanMove = true;
            Camera.LookAt(ActiveWorm.Worm.Position);
            Weapon.Reset();
            Timer.Seconds = 30;
        }


        public void EndTurn () {
            ActiveWorm.CanMove = false;
            Weapon.LockAndUnequip();
            Timer.Wait(new Time {Seconds = 0.5f});
        }


        public void AfterTurn () {
            // poison damage
        }


        public void Remove0Hp () {
            if (World.Remove0HpWorms()) Timer.Wait(new Time {Seconds = 0.5f});
        }

    }

}
