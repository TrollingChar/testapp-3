using System;
using System.Collections.Generic;
using Battle.Terrain;
using Battle.Terrain.Generation;
using Core;
using DataTransfer.Client;
using DataTransfer.Data;
using UnityEngine;
using Utils;
using Time = Core.Time;


namespace Battle.Experimental {

    public partial class BattleScene {

        // fill when command executes
        private Queue <TurnData> _turnDataQueue = new Queue <TurnData> ();
        private bool _syncReceived;


        private void StartGame (LandGen landGen) {
            World = new World (landGen, GetComponentInChildren <LandRenderer> ());
            var wormsPerTeam = new[] {10, 10, 10, 8, 7, 6, 5, 4};
            World.SpawnTeams (Teams, wormsPerTeam[Teams.Teams.Count - 1]);
            World.SpawnMines (15);
            _state = new AfterTurnState ();
            TweenTimer.Wait ();
            ArsenalPanel.Bind (Teams.MyTeam.Arsenal);
            LockArsenal ();
            
            OnGameStarted._ ();
        }


        public World World { get; set; }


        public void UpdateWorld () {
            UpdateWorld (null);
        }


        private void UpdateWorld (TurnData td) {
            UpdateWeapon (td);
            World.Update (td);
            UpdateTimers ();
        }


        public void SyncUpdateWorld () {
            if (MyTurn) {
                var td = TurnData.FromInput (false);
                The.Connection.Send (new TurnDataCCmd (td));
                UpdateWorld (td);
            }
            else if (_turnDataQueue.Count > 0) {
                UpdateWorld (_turnDataQueue.Dequeue ());
            }
        }


        public bool MyTurn {
            get { return Teams.MyTeamActive; }
        }


        private void UpdateTimers () {
            TweenTimer.Update ();
            TurnTimer.Update ();
            ControlTimer.Update ();
        }


        public event Action OnGameStarted;


        public void EndTurn () {
            ActiveWorm       = null;
            TurnTimer.Paused = false;
            TurnTimer.Ticks =
            ControlTimer.Ticks = 0;
            LockArsenal ();
        }


        public void InitRetreat (Time time) {
            if (ActiveWorm != null) ActiveWorm.Weapon = null;
            TurnTimer.Paused = false;
            TurnTimer.Time   = time;
            LockArsenal ();
        }

    }


}