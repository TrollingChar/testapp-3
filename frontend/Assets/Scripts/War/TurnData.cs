using Geometry;
using UnityEngine;


namespace War {

    public class TurnData {

        public bool w, a, s, d, mb;
        public XY xy;


        public TurnData () {
            w = Input.GetKey(KeyCode.W);
            a = Input.GetKey(KeyCode.A);
            s = Input.GetKey(KeyCode.S);
            d = Input.GetKey(KeyCode.D);
            mb = Input.GetMouseButton(0); // LMB
            xy = Core.bf.cameraWrapper.worldMousePosition;
        }


        public TurnData (byte flags, float x, float y) {
            w = (flags & 0x01) != 0;
            a = (flags & 0x02) != 0;
            s = (flags & 0x04) != 0;
            d = (flags & 0x08) != 0;
            mb = (flags & 0x10) != 0;
            xy = new XY(x, y);
        }

    }

}
