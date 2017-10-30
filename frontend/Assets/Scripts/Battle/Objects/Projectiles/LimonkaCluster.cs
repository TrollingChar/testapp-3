using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;
using Utils.Singleton;


namespace Battle.Objects.Projectiles {

    public class LimonkaCluster : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().LimonkaCluster, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive10Wide();
            Controller = new StandardController();
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
