using System;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Other.Crates;
using Core;
using DataTransfer.Data;
using DataTransfer.Server;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils.Random;
using Time = Core.Time;


namespace Battle.Experimental {

    public partial class BattleScene : MonoBehaviour {

        [SerializeField] public Text Hint;

        private GameInitData _initData;
        private Worm _activeWorm;




        public Worm ActiveWorm {
            get { return _activeWorm; }
            set {
                if (_activeWorm != null) {
                    _activeWorm.ArrowVisible = false;
                    _activeWorm.Weapon = null;
                }
                _activeWorm = value;
                if (value != null) value.ArrowVisible = true;
            }
        }
        
        public ArsenalPanel   ArsenalPanel { get; private set; }
        public CameraWrapper  Camera       { get; private set; }
        public CrateFactory   CrateFactory { get; set; }
        public NewTeamManager Teams        { get; set; }

        public NewTimer       TweenTimer   { get; set; }
        public NewTimer       TurnTimer    { get; set; }
        public NewTimer       ControlTimer { get; set; }
        


        public bool InputAvailable {
            get {
                if (MyTurn) {
                    var td = TurnData.FromInput (true);
                    return !td.Empty;
                }
                if (_turnDataQueue.Count > 0) {
                    var td = _turnDataQueue.Peek ();
                    return !td.Empty;
                }
                return false;
            }
        }


        public bool Synchronized   { get; set; }


        private void Awake () {
            The.Battle = this;

            _initData = (GameInitData) The.SceneSwitcher.Data[0];
            RNG.Init (_initData.Seed);

            The.Camera   =
            Camera       = GetComponentInChildren <CameraWrapper> ();
            ArsenalPanel = GetComponentInChildren <ArsenalPanel> ();
            CrateFactory = new CrateFactory ();
            TurnTimer    = new NewTimer ();
            TweenTimer   = new NewTimer ();
            ControlTimer = new NewTimer ();
            Teams        = new NewTeamManager (_initData.Players);
            
            NewTurnCmd.OnReceived += SetSyncFlag;
            TurnDataSCmd.OnReceived += EnqueueTurnData;

            StartGeneration ();
        }


        public void Update () {
            if (_state != null) UpdateGame ();
        }


        private void OnDestroy () {
            NewTurnCmd.OnReceived -= SetSyncFlag;
            TurnDataSCmd.OnReceived -= EnqueueTurnData;
        }


        private void EnqueueTurnData (TurnDataSCmd cmd) {
            _turnDataQueue.Enqueue (cmd.Data);
        }


        private void SetSyncFlag (NewTurnCmd cmd) {
            Synchronized = true;
        }


    }


}