
namespace W3 {
    class WormControllerJump : StandardController {

        public override void OnAdd () {
            obj.velocity = new XY(RNG.Float() - 0.5f, RNG.Float() - 0.5f) * 5;
        }

        public override void Update () {
            base.Update();
            Wait();
        }
    }
}