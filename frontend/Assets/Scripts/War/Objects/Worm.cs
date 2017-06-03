
using UnityEngine;
namespace W3 {
    public class Worm : Object {
        public Worm () { }

        public override void Detonate () {
            base.Detonate();
        }

        protected override void InitColliders () {

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