using Geometry;
using UnityEngine;
using War.Controllers;
using War.GameObjects;
using War.Physics.Collisions;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;


namespace War.Objects {

    public class Worm : Object {

        public const float headRadius = 5;
        public const float bodyHeight = 5;
        public WormGO spriteExtension;

        private Collider head, tail;

        public Worm () : base(60, 1) {}


        public override void Detonate () {
            base.Detonate();
        }


        protected override void InitColliders () {
            AddCollider(head = new CircleCollider(new XY(0, bodyHeight * 0.5f), headRadius));
            AddCollider(tail = new CircleCollider(new XY(0, bodyHeight * -0.5f), headRadius));
            //AddCollider(new BoxCollider(-5, 5, -2.5f, 2.5f));
        }


        protected override void InitController () {
            controller = new WormControllerJump();
        }


        protected override void InitSprite () {
            sprite = GameObject.Instantiate(Assets.Assets.worm);
            spriteExtension = sprite.GetComponent<WormGO>();
            spriteExtension.text = WormsNames.random();
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
