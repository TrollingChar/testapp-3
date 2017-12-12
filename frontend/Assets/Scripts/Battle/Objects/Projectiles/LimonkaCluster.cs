using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class LimonkaCluster : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.LimonkaCluster, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive10Wide();
            Controller = new StandardController();
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
