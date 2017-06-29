
using UnityEngine;
namespace W3 {
    public class Worm : Object {
        public const float headRadius = 5;
        public const float bodyHeight = 5;
        public WormGO spriteExtension;

        Collider head, tail;

        public Worm () : base() { }

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
            sprite = GameObject.Instantiate(Assets.worm);
            spriteExtension = sprite.GetComponent<WormGO>();
            spriteExtension.text = RNG.Bool() ? "Трарт" : "Ллалл";
        }

        protected override bool PassableFor (Object o) {
            return this == o;
        }

        public override void OnCollision (Collision c) {
            /*if (velocity.sqrLength > 100) {
                // fall damage
                controller = new WormControllerFall();
                spriteExtension.text = "Falling";
            } else */
            return;
            if (controller is WormControllerJump && c.collider1 == tail) {
                controller = new WormControllerWalk();
            }
        }
    }
}