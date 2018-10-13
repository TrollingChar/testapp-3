using System;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Other.Crates;
using Battle.UI;
using Core;
using DataTransfer.Data;
using UnityEngine;
using UnityEngine.UI;
using Utils.Random;
using Time = Core.Time;


namespace Battle.Experimental {

    public partial class NewBattleScene : MonoBehaviour {

        private                  GameInitData _initData;
        [SerializeField] private Text         _hint;
        private                  bool         _initialized;


        private void Awake () {
            The.Battle = this;

            _initData = (GameInitData) The.SceneSwitcher.Data[0];
            RNG.Init (_initData.Seed);

            The.Camera =
            Camera = GetComponentInChildren <CameraWrapper> ();
            ArsenalPanel = GetComponentInChildren <ArsenalPanel> ();

            StartGeneration ();
        }


        public void Update () {
            if (_initialized) {
                UpdateGame ();
            }
        }


        public ArsenalPanel  ArsenalPanel { get; private set; }
        public Worm          ActiveWorm   { get; set; }
        public CameraWrapper Camera       { get; private set; }

        public NewTimer TweenTimer   { get; set; }
        public NewTimer TurnTimer    { get; set; }
        public NewTimer ControlTimer { get; set; }
        public CrateFactory CrateFactory { get; set; }
        public NewTeamManager Teams { get; set; }


        public event Action OnBattleLoaded;

        public void EndTurn () { throw new NotImplementedException (); }

        public void InitRetreat (Time time) { throw new NotImplementedException (); }

        public void LockArsenal () { throw new NotImplementedException (); }

    }


}