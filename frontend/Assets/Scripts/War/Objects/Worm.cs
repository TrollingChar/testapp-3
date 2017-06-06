
using UnityEngine;
namespace W3 {
    public class Worm : Object {
        public const float headRadius = 5;
        public const float bodyHeight = 5;

        public Worm () { }

        public override void Detonate () {
            base.Detonate();
        }

        protected override void InitColliders () {
            AddCollider(new CircleCollider(new XY(0, bodyHeight * 0.5f), headRadius));
            AddCollider(new CircleCollider(new XY(0, bodyHeight * -0.5f), headRadius));
        }

        protected override void InitController () {
            controller = new StandardController();
        }

        protected override void InitSprite () {
            sprite = GameObject.Instantiate(Assets.worm);
        }

        protected override bool PassableFor (Object o) {
            return this == o;
        }
    }
}