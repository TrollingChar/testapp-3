﻿using Battle.Camera;
using Battle.Experimental;
using Core;


namespace Battle.State {

    public class StateReady : NewGameState {

        private readonly BattleScene _battle = The.Battle;


        public override void Init () {
            _battle.TweenTimer.Seconds = 5;
            _battle.TurnTimer.Seconds = 30;
            _battle.TurnTimer.Paused = true;
            var worm = _battle.ActiveWorm = _battle.Teams.ActiveTeam.ActiveWorm;
            _battle.UnlockArsenal ();
            
            _battle.Camera.Controller = new CameraController ();
            _battle.Camera.LookAt (worm.Position);
        }


        public override NewGameState Next () {
            return _battle.TweenTimer.Elapsed || _battle.InputAvailable ? new StateTurn () : null;
        }


        public override void Update () {
            _battle.SyncUpdateWorld ();
        }

    }

}