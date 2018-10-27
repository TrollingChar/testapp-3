using Battle.Objects.CollisionHandlers;
using Battle.Objects.Projectiles;
using Battle.Objects.Timers;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class LandmineCtrl : StandardCtrl {


        private readonly World _world = The.World;
        private Time _control;


        public LandmineCtrl () {
            WaitFlag = true;
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            if (Object.Velocity.SqrLength < 1) _control.Ticks++;
            if (_world.Time.Ticks % Time.TPS == 0) {
                if (_control.Seconds >= 0.9f) Object.CollisionHandler = new LandmineStickCH();
                _control.Ticks = 0;
            }
            var mine = (Landmine) Object;
            if (Object.Timer == null && mine.CheckWormsPresence(Landmine.ActivationRadius)) {
                Object.Timer = new LandmineDetonationTimer(new Time{Seconds = 2});
                Wait();
            }
        }

    }

}
