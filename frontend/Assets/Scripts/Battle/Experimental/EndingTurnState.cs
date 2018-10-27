using Core;


namespace Battle.Experimental {

    public class EndingTurnState : NewGameState {

        private readonly NewBattleScene _battle = The.Battle;


        public override void Init () {
            _battle.ActiveWorm = null;
            _battle.Teams.ActiveTeam.NextWorm ();
            
            _battle.TweenTimer.Wait ();
        }


        public override NewGameState Next () {
            return _battle.TweenTimer.Elapsed ? new AfterTurnState () : null;
        }


        public override void Update () {
            if (_battle.ControlTimer.Elapsed) _battle.UpdateWorld ();
            else                              _battle.SyncUpdateWorld ();
        }

    }

}