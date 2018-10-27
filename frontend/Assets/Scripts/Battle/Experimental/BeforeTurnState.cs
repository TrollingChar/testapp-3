using Core;
using DataTransfer.Client;
using Geometry;
using Utils.Random;


namespace Battle.Experimental {

    public class BeforeTurnState : NewGameState {

        private readonly NewBattleScene _battle = The.Battle;


        public override void Init () {
            _battle.Synchronized = false;
            The.Connection.Send (new TurnEndedCmd (_battle.Teams.MyTeam.WormsAlive > 0));
            
            for (int i = 0, count = _battle.Teams.Teams.Count; i < count; i++) {
                _battle.Teams.NextTeam ();
                // todo drop NUKES
                if (_battle.Teams.ActiveTeam.WormsAlive > 0) {
                    var world = _battle.World;
                    
                    // todo extinguish flames

                    world.Wind = RNG.Float () * 10f - 5f;
                    
                    _battle.TweenTimer.Wait ();
        
                    var crate = _battle.CrateFactory.GenCrate ();
                    if (crate == null) return;
                    var xy = new XY (RNG.Float () * world.Width, world.Height + 500);
                    world.Spawn (crate, xy);
                    return;
                }
            }
        }


        public override NewGameState Next () {
            return _battle.TweenTimer.Elapsed ? new SynchronizingState () : null;
        }


        public override void Update () {
            _battle.UpdateWorld ();
        }

    }

}