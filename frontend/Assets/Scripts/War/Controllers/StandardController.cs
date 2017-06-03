using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
    class StandardController : Controller {
        public override void Update () {
            obj.velocity.y += Core.bf.world.gravity;
        }
    }
}
