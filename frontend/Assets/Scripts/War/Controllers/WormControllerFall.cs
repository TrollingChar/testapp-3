
namespace W3 {
    class WormControllerFall : StandardController {

        int stillTime = 0;

        public override void Update () {
            if (obj.velocity.sqrLength < 1f) {
                if ((stillTime += 20) > 1000) {
                    obj.controller = new WormControllerWalk();
                }
            } else stillTime = 0;

            (obj as Worm).spriteExtension.text = stillTime.ToString();

            base.Update();
            Wait();
        }
    }
}
