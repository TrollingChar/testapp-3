using Geometry;
using UnityEngine;
using War.Controllers;
using War.GameObjects;
using War.Physics.Collisions;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace War.Objects {

    public class Worm : Object {

        public const float HeadRadius = 5;
        public const float BodyHeight = 5;
        public WormGO SpriteExtension;

        private Collider _head, _tail;

        public Worm () : base(60, 1) {}


        public override void Detonate () {
            base.Detonate();
        }


        protected override void InitColliders () {
            AddCollider(_head = new CircleCollider(new XY(0f, BodyHeight * 0.5f), HeadRadius));
            AddCollider(_tail = new CircleCollider(new XY(0f, BodyHeight * -0.5f), HeadRadius));
            //AddCollider(new BoxCollider(-5, 5, -2.5f, 2.5f));
        }


        protected override void InitController () {
            Controller = new WormControllerJump();
        }


        protected override void InitSprite () {
            Sprite = UnObject.Instantiate(Assets.Assets.Worm);
            SpriteExtension = Sprite.GetComponent<WormGO>();
            SpriteExtension.Text = WormsNames.Random();
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }


        public override void OnCollision (Collision c) {
            /*
            if (controller is WormControllerJump && c.collider1 == tail) {
                controller = new WormControllerWalk();
            }
             * */
        }

    }

}
