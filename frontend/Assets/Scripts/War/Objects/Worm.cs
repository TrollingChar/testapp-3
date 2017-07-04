using Geometry;
using UnityEngine;
using War.Objects.CollisionHandlers;
using War.Objects.Controllers;
using War.Objects.GameObjects;
using War.Physics.Collisions;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace War.Objects {

    public class Worm : Object {

        public const float HeadRadius = 5;
        public const float BodyHeight = 5;
        public WormGO SpriteExtension;

        public Collider Head { get; private set; }
        public Collider Tail { get; private set; }

        public Worm () : base(60, 1) {}


        public override void OnAdd () {
            Sprite = UnObject.Instantiate(Assets.Assets.Worm);
            SpriteExtension = Sprite.GetComponent<WormGO>();
            SpriteExtension.Text = WormsNames.Random();

            AddCollider(Head = new CircleCollider(new XY(0f, BodyHeight * 0.5f), HeadRadius));
            AddCollider(Tail = new CircleCollider(new XY(0f, BodyHeight * -0.5f), HeadRadius));
            //AddCollider(new BoxCollider(-5, 5, -2.5f, 2.5f));

            Controller = new WormControllerJump();
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }

    }

}
