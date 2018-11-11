using System.Linq;
using Battle.Experimental;
using Battle.Objects.Effects;
using Battle.Objects.Projectiles;
using Core;
using DataTransfer.Client;
using Geometry;
using Utils.Random;


namespace Battle.State {

    public class StateBeforeTurn : NewGameState {

        private readonly BattleScene _battle = The.Battle;
        private NewGameState _state;


        public override void Init () {
            _state = new StateSynchronizing();
            for (int i = 0, count = _battle.Teams.Teams.Count; i < count; i++) {
                
                var nukeTarget = (NukeTarget) _battle.World.Objects.FirstOrDefault (
                    o => {
                        var target = o as NukeTarget;
                        return target != null && !target.Despawned && target.ActivationI == _battle.Teams.I;
                    }
                );
                if (nukeTarget != null) {
                    _battle.World.Spawn (
                        new Nuke (),
                        nukeTarget.Position.WithY (_battle.World.Height + Balance.NukeExtraHeight),
                        new XY (0, -20f)
                    );
                    _battle.TweenTimer.Wait ();
                    nukeTarget.Despawn ();
                    _state = new StateRemoval (); // пропустить фазу где яд срабатывает
                    return;
                }
                
                _battle.Teams.NextTeam ();
                _battle.Synchronized = false;
                The.Connection.Send (new TurnEndedCmd (_battle.Teams.MyTeam.WormsAlive > 0));
            
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
            return _battle.TweenTimer.Elapsed ? _state : null;
        }


        public override void Update () {
            _battle.UpdateWorld ();
        }

    }

}