using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class GasGrenade : Object {

        public override void OnSpawn () {
            UnityEngine.Object.Instantiate(The.BattleAssets.Grenade, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new ExplosivePoison();
            Controller = new StandardCtrl {
                MagnetCoeff = 1,
                WaitFlag = true
            };
            Timer = new CallbackTimer(
                new Time {Seconds = 0.5f}, () => {
                    ((StandardCtrl) Controller).GasFlag = true;
                    Timer = new DetonationTimer(new Time{Seconds = 5});
                });
            CollisionHandler = new CollisionHandler();
        }

    }

}
