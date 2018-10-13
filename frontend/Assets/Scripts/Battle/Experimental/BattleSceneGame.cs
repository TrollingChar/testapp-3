using System;
using System.Collections.Generic;
using Battle.Terrain;
using Battle.Terrain.Generation;
using Core;
using DataTransfer.Client;
using DataTransfer.Data;


namespace Battle.Experimental {

    public partial class NewBattleScene {

        // fill when command executes
        private Queue <TurnData> _turnDataQueue = new Queue <TurnData> ();
        private bool _syncReceived;


        private void StartGame (LandGen landGen) {
            World = new World (landGen, GetComponentInChildren <LandRenderer> ());
            _state = new AfterTurnState ();
        }


        public World World { get; set; }


        public void UpdateWorld () {
            UpdateWorld (null);
        }


        private void UpdateWorld (TurnData td) {
            
        }


        public void SyncUpdateWorld () {
            if (MyTurn) {
                var td = TurnData.FromInput ();
                The.Connection.Send (new TurnDataCCmd (td));
                UpdateWorld (td);
            }
            else if (_turnDataQueue.Count > 0) {
                UpdateWorld (_turnDataQueue.Dequeue ());
            }
        }


        public bool MyTurn { get {throw new NotImplementedException();} }


        private void UpdateTimers () {
            TweenTimer.Update ();
            TurnTimer.Update ();
            ControlTimer.Update ();
        }

    }


}